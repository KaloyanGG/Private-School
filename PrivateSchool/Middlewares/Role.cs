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

        public async Task InvokeAsync(HttpContext context, PrivateSchoolDBContext db, RoleManager<IdentityRole> roleManager)
        {

            foreach (KeyValuePair<int, string> KeyValue in StaticData.Roles)
            {
                if (!roleManager.RoleExistsAsync(KeyValue.Value).Result)
                {
                    await roleManager.CreateAsync(new IdentityRole(KeyValue.Value));
                }
            }

            await _next(context);

        }

    }
}
