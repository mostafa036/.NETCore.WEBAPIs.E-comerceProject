using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Identity;

namespace Talabat.Repository.Identity
{
    public class AppIdentityDbContextSedd
    {
        public static async Task SeedUserAsyns(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "ahmed naser",
                    Email = "ahmed.naser@linkdev.com",
                    UserName = "ahmed.naser",
                    PhoneNumber = "01235463733"
                };
                await userManager.CreateAsync(user,"Pa$$w0rd");
            }       
        }
    }
}