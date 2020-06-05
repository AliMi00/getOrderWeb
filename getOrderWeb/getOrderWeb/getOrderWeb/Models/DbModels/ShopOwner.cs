using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace getOrderWeb.Models.DbModels
{
    public class ShopOwner : IdentityUser
    {
        public string PostCode { get; set; }
        public string Address { get; set; }

        public Shop Shop { get; set; }

    }
}
