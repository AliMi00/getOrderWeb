using getOrderWeb.Models.DbModels;
using getOrderWeb.Models.ViewModels;

namespace getOrderWeb.Services
{
    public interface IOrderServices
    {
        Customer getCustomer(int customerId);
        Order GetOrder(string username, int? orderId = null, OrderStatusTypes? status = OrderStatusTypes.Open, bool withIncludes = false);
        Product GetProduct(int productId);
        ShopOwner GetShopOwner(string username);
        ProductAddedResponseViewModel SaveOrder(string username, int productId, int quantity = 1, int customerId = 1);
        int validateOrders();
    }
}