using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Talabat.Core.Entites.Identity;

namespace Talabat.APIs.Services
{
    public interface ITokenServices
    {
     Task <string> CreatedToken (AppUser user , UserManager<AppUser> userManager);
    }
}
