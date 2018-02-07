using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppPizza.Models
{
    public class SeedUserAndRole
    {
        private const string userAdmin = "admin@pizza.com";
        private const string userPassword = "Zu1234!";

        public static async Task EnsurePopulateUserAdmin(IApplicationBuilder app)
        {
            UserManager<ApplicationUser> userManager = app.ApplicationServices.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();

            ApplicationUser user = await userManager.FindByEmailAsync("admin@pizza.com");

            if(user == null)
            {
                user = new ApplicationUser() { UserName = userAdmin, Email = userAdmin };
                await userManager.CreateAsync(user, userPassword);
                IdentityRole role = await roleManager.FindByNameAsync("Admin");
                await userManager.AddToRoleAsync(user, role.Name);

            }
        }

        public static async Task EnsurePopulateRole(IApplicationBuilder app)
        {
            List<string> roles = new List<string>
            {
                "Admin",
                "Client"
            };

            RoleManager<IdentityRole> roleManager = app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var r in roles)
            {
                IdentityRole role = await roleManager.FindByNameAsync(r);

                if(role == null)
                {
                    role = new IdentityRole(r);
                    await roleManager.CreateAsync(role);
                }
            }
        }

    }
}
