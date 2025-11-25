using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatPro.Api.Core.Entities.IdentityModule;

namespace TalabatPro.Api.Repository.IdentityData.SeedingIdentity
{
    public static class IdentityContextSeeding
    {
        public  static async Task SeedUser(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var Admin = new AppUser()
                {
                    DisplayName = "Admin",
                    Email = "admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "01153792705"
                };
                var result = await userManager.CreateAsync(Admin, "Admin@123"); // باسورد قوي
                await userManager.AddToRoleAsync(Admin, "Admin");
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error: {error.Description}");
                    }
                }
            }
        }
    }
}
