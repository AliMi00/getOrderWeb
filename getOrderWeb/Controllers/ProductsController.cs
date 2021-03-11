using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using getOrderWeb.Data;
using getOrderWeb.Models.DbModels;
using getOrderWeb.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace getOrderWeb.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductServices productServices;
        string Username
        {
            get
            {
                return User.FindFirstValue(ClaimTypes.Name);
            }
        }

        public ProductsController(IProductServices productServices)
        {
            this.productServices = productServices;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View(productServices.GetProducts());
        }
        //GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productServices.GetProduct((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Description,SellPrice,BuyPrice,")] Product product)
        {
            if (ModelState.IsValid)
            {
                
                productServices.SaveProduct(Username, product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

    }
}
