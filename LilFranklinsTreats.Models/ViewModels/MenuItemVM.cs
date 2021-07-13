using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LilFranklinsTreats.Models.ViewModels
{
    public class MenuItemVM
    {
        public MenuItem MenuItem { get; set; }
        public  IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }
    }
}
