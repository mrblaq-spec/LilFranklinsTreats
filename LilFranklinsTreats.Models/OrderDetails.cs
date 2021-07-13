using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilFranklinsTreats.Models
{
    public class OrderDetails
    {
        /// <summary>
        /// unique order details id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// order id of the current order being placed.
        /// </summary>
        [Required]
        public int OrderId { get; set; }

        /// <summary>
        /// menuItem Id of the current order being placed.
        /// </summary>
        [Required]
        public int MenuItemId { get; set; }

        /// <summary>
        /// Foreign key of the order id transaction table OrderHeader.
        /// </summary>
        [ForeignKey("OrderId")]
        public virtual OrderHeader OrderHeader { get; set; }

        /// <summary>
        /// menuItemId of the current item in order.
        /// </summary>
        [ForeignKey("MenuItemId")]
        public virtual MenuItem MenuItem { get; set; }

        public int Count { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
