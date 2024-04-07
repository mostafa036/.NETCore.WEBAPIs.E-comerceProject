using AutoMapper;
using AutoMapper.Execution;
using Microsoft.Extensions.Configuration;
using Talabat.APIs.Dtos;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.APIs.Helpers
{
    public class OrderPictureUrlResponse : IValueResolver<OrderItem, OrderToReturneDto, string>
    {
        public IConfiguration Configuration { get; }

        public OrderPictureUrlResponse(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderToReturneDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Proudect.PictureUrl))
                return $"{Configuration["BaseApiUrl"]}{source.Proudect.PictureUrl}";
            return null;
        }
    }
}
