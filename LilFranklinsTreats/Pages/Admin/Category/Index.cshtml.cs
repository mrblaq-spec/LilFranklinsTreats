using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LilFranklinsTreats.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LilFranklinsTreats.Pages.Admin.Category
{
    [Authorize(Roles = SD.ManagerRole)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
