using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace getOrderWeb.Models.ViewModels
{
    public class ProductAddedResponseViewModel
    {
        public string Message { get; set; }
        public bool Succeed { get; set; }
        public int orderId { get; set; }
    }
}
