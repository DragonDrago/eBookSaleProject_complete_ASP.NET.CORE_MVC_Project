using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using eBookSaleProject.Data.Enum;
using eBookSaleProject.Models;
using Microsoft.AspNetCore.Identity;
using eBookSaleProject.Data.Static;

namespace eBookSaleProject.Data
{
    public class AppDbInitializer
    {

        public static async Task SeedUsersAndRolesAsync (IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users -Admin
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };

                    IdentityResult newUserResponse = await userManager.CreateAsync(newAdminUser, "@Admin123");

                    if (newUserResponse.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                    }

                }

                //App -User
                string appUserEmail = "user@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                        
                    IdentityResult newUserResponse = await userManager.CreateAsync(newAppUser, "@User123");

                    if (newUserResponse.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                    }
                }
            }
        }
    }
}
