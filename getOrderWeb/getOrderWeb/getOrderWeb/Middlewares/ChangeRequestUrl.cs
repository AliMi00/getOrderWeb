using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace getOrderWeb.Middlewares
{
    //for load balancing adn reverse proxy
    public class ChangeRequestUrl : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Request.Host = new HostString("https://localhost", 10000);
            return next.Invoke(context);
        }
    }
}
