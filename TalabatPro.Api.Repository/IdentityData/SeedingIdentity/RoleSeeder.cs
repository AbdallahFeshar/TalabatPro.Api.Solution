using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalabatPro.Api.Repository.IdentityData.SeedingIdentity
{
    public static class RoleSeeder
    {
        //RoleManager<IdentityRole> roleManager
        public static async Task SeedRoles(RoleManager<IdentityRole>roleManager)
        {
            if(!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            if(!await roleManager.RoleExistsAsync("Customer"))
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            //if (!await roleManager.RoleExistsAsync("Admin"))
            //    await roleManager.CreateAsync(new IdentityRole("Admin"));
            //
            //if (!await roleManager.RoleExistsAsync("Customer"))
            // await roleManager.CreateAsync(new IdentityRole("Customer"));
        }
    }
}
