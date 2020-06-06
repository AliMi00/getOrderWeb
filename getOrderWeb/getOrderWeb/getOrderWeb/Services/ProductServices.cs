using getOrderWeb.Data;
using getOrderWeb.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace getOrderWeb.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IApplicationDbContext db;
        public ProductServices(IApplicationDbContext _db)
        {
            db = _db;
        }
        public List<ProductViewModel> GetProducts(int CategoryId, bool Delete = false)
        {
            return db.Products
                .Where(p => !p.RemoveDate.HasValue &&
                            !p.DisableDate.HasValue &&
                            p.ProductCategories
                            .Any(pc =>
                                 pc.Cateory.Id == CategoryId &&
                                 !pc.DisableDate.HasValue &&
                                 !pc.RemoveDate.HasValue &&
                                 !pc.Cateory.RemoveDate.HasValue &&
                                 !pc.Cateory.DisableDate.HasValue
                                 )
                       )
                .Select(x => new ProductViewModel()
                {
                    ImageAddress = x.PictureAddress,
                    Title = x.Title,
                    Id = x.Id,
                    price = x.SellPrice
                }).ToList();
        }
        public List<ProductViewModel> GetProducts(string CategoryName, bool Delete = false)
        {
            return GetProducts(GetCategoryId(CategoryName), Delete);
        }
        public int GetCategoryId(string CategoryName)
        {
            return db.Categories.Single(c => c.Title == CategoryName).Id;
        }
    }
}
