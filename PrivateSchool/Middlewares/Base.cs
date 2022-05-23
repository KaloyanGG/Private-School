using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Middlewares
{
    public static class Base 
    {
        public static IApplicationBuilder UseRoleMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Role>();
        } 


    }
}
