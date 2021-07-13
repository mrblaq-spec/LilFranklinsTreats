using LilFranklinsTreats.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LilFranklinsTreats.DataAccess.Data.Repository.IRepository
{
    public interface IMenuItemRepository: IRepository<MenuItem>
    {
        void Update(MenuItem menuItem);
    }
}
