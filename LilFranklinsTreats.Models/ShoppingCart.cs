using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilFranklinsTreats.Models
{
    public class ShoppingCart
    {
        /// <summary>
        /// bind with foreignKey references.
        /// </summary>
        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// bind with foreignKey references.
        /// </summary>
        [NotMapped]
        [ForeignKey("MenuItemId")]
        public virtual MenuItem MenuItem { get; set; }

        /// <summary>
        /// shopping cart Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// gets the GUID string user id.
        /// </summary>
        public string ApplicationUserId { get; set; }

        /// <summary>
        /// gets the menuItem id.
        /// </summary>
        public int MenuItemId { get; set; }

        /// <summary>
        /// range validation, w/ custom error message.
        /// </summary>
        [Range(1, 100, ErrorMessage = "Please select a count between 1 and 100")]
        public int Count { get; set; }

        /// <summary>
        /// creates a new instance of a shopping cart
        /// </summary>
        public ShoppingCart()
        {
            Count = 1;
        }

    }
}
