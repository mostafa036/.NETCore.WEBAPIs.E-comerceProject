using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.APIs.Services;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.IRepositories;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Sevices;

namespace Talabat.APIs.Extenctions
{
    public static class  ApplicationServicesExtenstions
    {
        public static IServiceCollection addapplicationservicse ( this IServiceCollection services)
        {
            services.AddScoped<TalabatContext>();

            services.AddSingleton<IResponseCacheServices, ResponseCacheService>();

            services.AddScoped<IPaymentServices, PaymentinServices>();

            services.AddScoped<Iunitofwork, Unitofwork>();

            services.AddScoped<IOrderServices, OrderServices>();

            services.AddScoped<ITokenServices, TokenServices>();

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
            services.AddAutoMapper(typeof(MappingProfiles));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Count() > 0)
                                                         .SelectMany(M => M.Value.Errors)
                                                         .Select(m => m.ErrorMessage)
                                                         .ToArray();

                    var validationerrorMessage = new ApiValidtionErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationerrorMessage);

                };
            });

            return services;
        }
    }
}
