using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entites.Identity;
using Talabat.Repository.Identity;

namespace Talabat.APIs.Extenstions
{
    public static class IdentityServisesEtenstion
    {
    public static IServiceCollection addIdentityservises(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {

            })
            .AddEntityFrameworkStores<AppIdentityrDbContext>();

            services.AddAuthentication(options=>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                };
            });

            return services;

        }
    }
}
