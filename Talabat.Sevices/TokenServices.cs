using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.APIs.Services;
using Talabat.Core.Entites.Identity;

namespace Talabat.Sevices
{
    public class TokenServices : ITokenServices
    {
        public IConfiguration Configuration { get; }
        public TokenServices(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public async Task<string> CreatedToken(AppUser user, UserManager<AppUser> userManager)
        {
            var authCliams = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName,user.DisplayName)
            };
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
                authCliams.Add(new Claim(ClaimTypes.Role, role));

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]));

            var tokeen = new JwtSecurityToken(
                issuer: Configuration["JWT:ValidIssuer"],
                audience: Configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(Configuration["JWT:DurationInDays"])),
                claims: authCliams,
                signingCredentials: new SigningCredentials(authKey , SecurityAlgorithms.HmacSha256Signature)
                );
                
            return new JwtSecurityTokenHandler().WriteToken(tokeen);
        }
    }
}
