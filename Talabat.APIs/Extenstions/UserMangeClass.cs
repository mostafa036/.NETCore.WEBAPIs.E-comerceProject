using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.Core.Entites.Identity;

namespace Talabat.APIs.Extenstions
{
    public static class UserMangeClass
    {
        public static async Task<AppUser> finduserwithaddressbeemailasync(this UserManager<AppUser>userManager, ClaimsPrincipal User   )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(U => U.Address).SingleOrDefaultAsync(U => U.Email == email);
            return user;
        }
    }
}
