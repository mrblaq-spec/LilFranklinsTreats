using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LilFranklinsTreats.DataAccess.Data.Repository.IRepository;
using LilFranklinsTreats.Models;
using LilFranklinsTreats.Models.ViewModels;
using LilFranklinsTreats.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;

namespace LilFranklinsTreats.Pages.Admin.Order
{
    public class OrderDetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// instantiates an instance of an OrderDetailsModel object
        /// </summary>
        /// <param name="unitOfWork">repository</param>
        public OrderDetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// binds the order detail view model properties
        /// </summary>
        [BindProperty]
        public OrderDetailsVM OrderDetailsVM { get; set; }

        /// <summary>
        /// displays the list of order details for each customer
        /// </summary>
        /// <param name="id">User id of the current User's order detail list</param>
        public void OnGet(int id)
        {
            // creates a new order details view model object.
            OrderDetailsVM = new OrderDetailsVM()
            {
                // load properties of the object into memory from db using query.
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(m => m.Id == id),
                OrderDetails = _unitOfWork.OrderDetails.GetAll(m => m.OrderId == id).ToList()
            };

            // return correct order details view model according to requested or logged in user
            // if user is not an admin i.e. (Manager, Front Desk, Developer)
            OrderDetailsVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault
                (u => u.Id == OrderDetailsVM.OrderHeader.UserId);
        }

        
        public IActionResult OnPostOrderConfirm(int orderId)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderId);
            orderHeader.Status = SD.StatusCompleted;
            _unitOfWork.Save();
            return RedirectToPage("OrderList");
        }
        public IActionResult OnPostOrderCancel(int orderId)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderId);
            orderHeader.Status = SD.StatusCancelled;
            _unitOfWork.Save();
            return RedirectToPage("OrderList");
        }
        public IActionResult OnPostOrderRefund(int orderId)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderId);
            // refund the amount to customer.
            var options = new RefundCreateOptions
            {
                Amount = Convert.ToInt32(orderHeader.OrderTotal * 100),
                Reason = RefundReasons.RequestedByCustomer,
                Charge = orderHeader.TransactionId,
            };
            var service = new RefundService();
            Refund refund = service.Create(options);

            orderHeader.Status = SD.StatusRefunded;
            _unitOfWork.Save();
            return RedirectToPage("OrderList");
        }
    }
}
