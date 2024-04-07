using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.APIs.Dtos;
using Talabat.Core.Entites;

namespace Talabat.APIs.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturneDto, string>
    {
        public IConfiguration Configuration { get; }
        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string Resolve(Product source, ProductToReturneDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{Configuration["BaseApiUrl"]}{source.PictureUrl}";
            return null;
        }
    }
}
