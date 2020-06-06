using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace getOrderWeb.Middlewares
{
    public static class ChangeRequestUrlExtention
    {
        public static IApplicationBuilder ChangeRequestUrl(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ChangeRequestUrl>();
        }
    }
}
