using LilFranklinsTreats.DataAccess.Data.Repository.IRepository;
using LilFranklinsTreats.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LilFranklinsTreats.DataAccess.Data.Repository
{
    public class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public FoodTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetFoodTypeListForDropDown()
        {
            return _db.FoodType.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(FoodType foodtype)
        {
            var objFromDb = _db.FoodType.FirstOrDefault(s => s.Id == foodtype.Id);

            objFromDb.Name = foodtype.Name;

            _db.SaveChanges();
        }
    }
}
