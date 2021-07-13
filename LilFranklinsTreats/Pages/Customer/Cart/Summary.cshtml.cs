using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LilFranklinsTreats.DataAccess.Data.Repository.IRepository;
using LilFranklinsTreats.Models;
using LilFranklinsTreats.Models.ViewModels;
using LilFranklinsTreats.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;

namespace LilFranklinsTreats.Pages.Customer.Cart
{
    public class SummaryModel : PageModel
    {
        /// <summary>
        /// unit of work repository.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public OrderDetailsCart DetailCart { get; set; }

        /// <summary>
        /// repository responsible for all unit of work
        /// </summary>
        /// <param name="unitOfWork">unit of work repository</param>
        public SummaryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult OnGet()
        {
            DetailCart = new OrderDetailsCart()
            {
                OrderHeader = new OrderHeader()
            };

            DetailCart.OrderHeader.OrderTotal = 0;

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            IEnumerable<ShoppingCart> cart = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value);

            if (cart != null)
            {
                DetailCart.ListCart = cart.ToList();
            }

            foreach (var cartList in DetailCart.ListCart)
            {
                cartList.MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(m => m.Id == cartList.MenuItemId);
                DetailCart.OrderHeader.OrderTotal += cartList.MenuItem.Price * cartList.Count;
            }

            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(c => c.Id == claim.Value);
            DetailCart.OrderHeader.PickupName = applicationUser.FirstName;
            DetailCart.OrderHeader.PickUpTime = DateTime.Now;
            DetailCart.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
            return Page();
        }

        public IActionResult OnPost(string stripeToken)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            DetailCart.ListCart = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value).ToList();

            DetailCart.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            DetailCart.OrderHeader.OrderDate = DateTime.Now;
            DetailCart.OrderHeader.UserId = claim.Value;
            DetailCart.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            DetailCart.OrderHeader.Status = SD.PaymentStatusPending;
            DetailCart.OrderHeader.PickUpTime = Convert.ToDateTime
                (DetailCart.OrderHeader.PickUpDate.ToShortDateString()
                + " " + DetailCart.OrderHeader.PickUpTime.ToShortTimeString());

            List<OrderDetails> orderDetailsList = new();
            _unitOfWork.OrderHeader.Add(DetailCart.OrderHeader);
            _unitOfWork.Save();

            foreach (var item in DetailCart.ListCart)
            {
                item.MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(m => m.Id == item.MenuItemId);
                OrderDetails orderDetails = new()
                {
                    MenuItemId = item.MenuItemId,
                    OrderId = DetailCart.OrderHeader.Id,
                    Description = item.MenuItem.Description,
                    Name = item.MenuItem.Name,
                    Price = item.MenuItem.Price,
                    Count = item.Count
                };
                DetailCart.OrderHeader.OrderTotal += (orderDetails.Count * orderDetails.Price);
                _unitOfWork.OrderDetails.Add(orderDetails);
            }

            DetailCart.OrderHeader.OrderTotal = Convert.ToDouble(String.Format("{0:.##}", DetailCart.OrderHeader.OrderTotal));
            _unitOfWork.ShoppingCart.RemoveRange(DetailCart.ListCart);
            HttpContext.Session.SetInt32(SD.ShoppingCart, 0);
            _unitOfWork.Save();

            StripeConfiguration.ApiKey = "sk_test_51JAU43ItXl4EX6UVryFSYRVoyrbZkX5ygnJUnXSND6vOg7E0U0YcSqb7FBt2abtioFhBlebYSa54zJ8D32scIDA100aa3RKbWM";

            if (stripeToken != null)
            {
                var options = new ChargeCreateOptions
                {
                    // Amount is in cents so it must be multiplied by 100
                    Amount = Convert.ToInt32(DetailCart.OrderHeader.OrderTotal * 100),
                    Currency = "usd",
                    Source = stripeToken,
                    Description = "Order ID : " + DetailCart.OrderHeader.Id
                };
                var service = new ChargeService();
                Charge charge = service.Create(options);

                DetailCart.OrderHeader.TransactionId = charge.Id;

                if (charge.Status.ToLower() == "succeeded")
                {
                    // email
                    DetailCart.OrderHeader.PaymentStatus = SD.PaymentStatusApproved;
                    DetailCart.OrderHeader.Status = SD.StatusSubmitted;
                }
                else
                {
                    DetailCart.OrderHeader.PaymentStatus = SD.PaymentStatusRejected;
                }
            } 
            else
            {
                DetailCart.OrderHeader.PaymentStatus = SD.PaymentStatusRejected;
            }
            _unitOfWork.Save();

            return RedirectToPage("/Customer/Cart/OrderConfirmation", new { id = DetailCart.OrderHeader.Id });
        }
    }
}
