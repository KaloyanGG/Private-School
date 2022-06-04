using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PrivateSchool.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrivateSchool.Middlewares
{
    public class Role
    {

        private readonly RequestDelegate _next;

        public Role(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, RoleManager<IdentityRole> roleManager)
        {
            foreach (string role in StaticData.Roles)
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            await _next(context);
        }
    }
}
