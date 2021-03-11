using getOrderWeb.Models.DbModels;
using getOrderWeb.Models.ViewModels;
using System.Collections.Generic;

namespace getOrderWeb.Services
{
    public interface IProductServices
    {
        int GetCategoryId(string CategoryName);
        List<ProductViewModel> GetProducts(int CategoryId, bool Delete = false);
        List<ProductViewModel> GetProducts(string CategoryName, bool Delete = false);
        List<ProductViewModel> GetProducts(bool Delete = false);
        bool SaveProduct(string username, Product product);
        Product GetProduct(int id);

    }
}