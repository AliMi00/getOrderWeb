using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace getOrderWeb.Models.DbModels
{
    public class Product
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SellPrice { get; set; }
        public int BuyPrice { get; set; }

        public string PictureAddress { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime?  DisableDate { get; set; }
        public DateTime? RemoveDate { get; set; }
        public Shop Shop { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
