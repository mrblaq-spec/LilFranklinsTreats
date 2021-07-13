using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilFranklinsTreats.Models
{
    public class OrderHeader
    {
        /// <summary>
        /// unique identifier for order header
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// required user Id foreign key identifier
        /// </summary>
        [Required]
        public string UserId { get; set; }
        
        /// <summary>
        /// transactional id for column name
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// when the order was placed
        /// </summary>
        [Required]
        [Display(Name = "Order Time")]
        public DateTime OrderDate { get; set; }

        /// <summary>
        ///  total for order being placed.
        /// </summary>
        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name ="Order Total")]
        public double OrderTotal { get; set; }

        /// <summary>
        ///  time for order to be picked up.
        /// </summary>
        [Required]
        [Display(Name = "Pick Up Time")]
        public DateTime PickUpTime { get; set; }

        /// <summary>
        ///  date for order to be picked up.
        /// </summary>
        [Required]
        [NotMapped]
        public DateTime PickUpDate { get; set; }

        /// <summary>
        ///  order status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// payment status via stripe
        /// </summary>
        public string PaymentStatus { get; set; }

        /// <summary>
        /// user comments
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// display name of person picking up order.
        /// </summary>
        [Display(Name = "Pickup Name")]
        public string PickupName { get; set; }

        /// <summary>
        /// phone nunmber of person picking up order.
        /// </summary>
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// transaction ID of completed process.
        /// </summary>
        public string TransactionId {get; set; }

    }
}
