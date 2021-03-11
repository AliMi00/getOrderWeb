using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace getOrderWeb.Models.DbModels
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int AmountSell { get; set; }
        public int AmountCost { get; set; }
        public int Profit { get; set; }
        public Customer Customer { get; set; }
        public OrderStatusTypes OrderStatus { get; set; }
        #nullable enable
        public string? Authority { get; set; }
        #nullable restore
        public DateTime? PaymentDate { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ShopOwner ShopOwner { get; set; }

    }
}
