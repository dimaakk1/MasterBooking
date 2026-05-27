using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.DbContext
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider services)
        {
            var roleManager =
                services.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Client", "Master", "Admin" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
