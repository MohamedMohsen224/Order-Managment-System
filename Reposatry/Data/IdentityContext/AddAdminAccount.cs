using Core.Models.IdentityServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposatry.Data.IdentityContext
{
    public static class AddAdminAccount
    {
        public static async Task AddAdminAccountAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
           if (await userManager.FindByEmailAsync("Admin@Store.com")== null)
            {
                var user = new AppUser
                   {
                     UserName = "Admin",
                    Email = "Admin@store.com",
                    EmailConfirmed = true
                   };
               var Result= await userManager.CreateAsync(user, "Mo010450$");
                if (Result.Succeeded)
                {
                    if (await roleManager.FindByNameAsync("Admin") == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                    }
                    await userManager.AddToRoleAsync(user, "Admin");
                }

           }
            
        }
    }
}
