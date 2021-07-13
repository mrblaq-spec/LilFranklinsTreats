using LilFranklinsTreats.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LilFranklinsTreats.DataAccess.Data.Repository.IRepository
{
    public interface IOrderHeaderRepository: IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);

    }
}
