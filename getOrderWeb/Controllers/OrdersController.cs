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
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Security.Claims;

namespace getOrderWeb.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderServices orderServices;
        private readonly IApplicationDbContext _context;
        string Username
        {
            get
            {
                return User.FindFirstValue(ClaimTypes.Name);
            }
        }
        public OrdersController(IOrderServices orderServices,IApplicationDbContext context)
        {
            _context = context;
            this.orderServices = orderServices;
        }

        // GET: Orders
        public IActionResult Index()
        {
            return View(orderServices.GetOrder(Username));
        }

    }
}
