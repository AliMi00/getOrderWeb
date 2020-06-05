using getOrderWeb.Models.ViewModels;
using System.Collections.Generic;

namespace getOrderWeb.Services
{
    public interface IProductServices
    {
        int GetCategoryId(string CategoryName);
        List<ProductViewModel> GetProducts(int CategoryId, bool Delete = false);
        List<ProductViewModel> GetProducts(string CategoryName, bool Delete = false);
    }
}