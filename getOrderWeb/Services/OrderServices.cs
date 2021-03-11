using getOrderWeb.Data;
using getOrderWeb.Models.DbModels;
using getOrderWeb.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace getOrderWeb.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IApplicationDbContext db;
        public OrderServices(IApplicationDbContext _db)
        {
            db = _db;
        }

        //add order details to order or creat and add 
        //customer id 1 is defualt custome for those who dont want use customer option
        public ProductAddedResponseViewModel SaveOrder(string username, int productId, int quantity = 1, int customerId = 1)
        {
            var responseModel = new ProductAddedResponseViewModel();
            var shopOwner = this.GetShopOwner(username);
            var product = this.GetProduct(productId, shopOwner);
            var customer = this.getCustomer(customerId);
            Order order;

            if (shopOwner == null)
            {
                responseModel.Message = "Shop Owner not Found";
                responseModel.Succeed = false;
                return responseModel;
            }
            //check for product
            if (product == null)
            {
                responseModel.Message = " product is wrong";
                responseModel.Succeed = false;
                return responseModel;
            }
            //check for customer
            if (customer == null)
            {
                responseModel.Message = " customer is wrong";
                responseModel.Succeed = false;
                return responseModel;
            }
            //get order or create one if is not 
            order = GetOrder(username);
            if (order == null)
            {
                order = new Order()
                {
                    OrderDate = DateTime.Now,
                    ShopOwner = shopOwner,
                    Customer = customer
                };
                db.Orders.Add(order);
            }

            var orderDetail = new OrderDetail()
            {
                Order = order,
                Product = product,
                UnitPriceSell = product.SellPrice,
                UnitPriceCost = product.BuyPrice,
                Tax = 0,
                Discount = 0,
                Quantity = quantity,
                CreationDate = DateTime.Now
            };
            db.OrderDetails.Add(orderDetail);
            order.AmountSell = quantity * product.SellPrice;
            order.AmountCost = quantity * product.BuyPrice;
            db.SaveChanges();

            responseModel.Message = "Product added to order";
            responseModel.Succeed = true;
            responseModel.orderId = order.Id;

            return responseModel;
        }

        public ShopOwner GetShopOwner(string username)
        {
            return db.ShopOwners.SingleOrDefault(s => s.UserName == username);
        }

        public Product GetProduct(int productId , ShopOwner shopOwner)
        {
            return db.Products.SingleOrDefault(p => p.Id == productId && p.ShopOwner.Id == shopOwner.Id && !p.DisableDate.HasValue && !p.RemoveDate.HasValue );
        }

        public Order GetOrder(string username, int? orderId = null, OrderStatusTypes? status = OrderStatusTypes.Open, bool withIncludes = false)
        {
            Order order = null;
            IQueryable<Order> orders = db.Orders.Where(x => x.ShopOwner.UserName == username);
            if (orderId.HasValue)
            {
                orders = orders.Where(x => x.Id == orderId.Value);
            }
            if (status.HasValue)
            {
                orders = orders.Where(x => x.OrderStatus == status.Value);
            }
            if (withIncludes)
            {
                orders = orders.Include(x => x.OrderDetails)
                               .ThenInclude(x => x.Product);
            }
            var ordersList = orders.ToList();
            //check and fix if there is unfinished order
            switch (ordersList.Count())
            {
                case 0:
                    return order;
                case 1:
                    return orders.SingleOrDefault();
                default:
                    foreach (var item in orders)
                    {
                        item.OrderStatus = OrderStatusTypes.NeedReview;
                    }
                    db.SaveChanges();
                    return order;
            }
        }

        public Customer getCustomer(int customerId)
        {
            return db.Customers.SingleOrDefault(c => c.Id == customerId);

        }

        public int validateOrders()
        {
            var orders = db.Orders.Include(o => o.OrderDetails).ToList();
            //In OrderStatusType Enum, there is a 1000 step distance between temp values and
            //real values, so they can simply turn into each other
            //for more info,
            ///<seealso cref="OrderStatusTypes"/>
            const int Status_Temp_Distance = 1000;
            orders.AsParallel()
                .ForAll(o =>
                {
                    var detailsPriceSum = o.OrderDetails.Where(od => !od.DeleteDate.HasValue).Sum(odp => odp.UnitPriceCost);
                    if (o.AmountCost != detailsPriceSum)
                    {
                        o.AmountCost = detailsPriceSum;
                        o.OrderStatus = o.OrderStatus + Status_Temp_Distance;

                    }
                });
            var totalChanged = 0;
            orders.ForEach(o =>
            {
                if ((int)o.OrderStatus >= Status_Temp_Distance)
                {
                    totalChanged++;
                    o.OrderStatus = o.OrderStatus - Status_Temp_Distance;
                }
            });

            db.SaveChanges();
            return totalChanged;

        }


    }
}
