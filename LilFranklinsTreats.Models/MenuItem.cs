using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LilFranklinsTreats.Models
{
    public class MenuItem
    {
        // Menu Item unique id.
        [Key]
        public int Id { get; set; }

        // Name of menu item.
        [Required]
        public string Name { get; set; }

        // Description of menu item.
        [Required]
        public string Description { get; set; }

        // Image of menu item.
        public string Image { get; set; }
        
        // dollar range of price for menu item.
        [Range(1, int.MaxValue, ErrorMessage ="Price should be greater than $1")]
        public double Price { get; set; }

        // Name of category type for menu item.
        [Display(Name ="Category Type")]
        public int CategoryId { get; set; }

        // Corresponding categoryId to reference Category table.
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        
        // Name of food type for menu item.
        [Display(Name = "Food Type")]
        public int FoodTypeId { get; set; }

        // Correspondng foodtypeId to reference Foodtype table.
        [ForeignKey("FoodTypeId")]
        public virtual FoodType FoodType { get; set; }
    }
}
