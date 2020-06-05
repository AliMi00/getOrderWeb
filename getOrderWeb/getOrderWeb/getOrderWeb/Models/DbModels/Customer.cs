using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace getOrderWeb.Models.DbModels
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
#nullable enable
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
#nullable restore
        public ICollection<Order> Orders { get; set; }



    }
}
