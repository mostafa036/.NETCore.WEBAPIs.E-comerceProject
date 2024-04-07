using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturneDto>()
                .ForMember(d => d.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
                .ForMember(d => d.ProductType, O => O.MapFrom(S => S.ProductType.Name))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<AddressDto, Core.Entites.Identity.Address>().ReverseMap();

            CreateMap<AddressDto, Core.Entites.OrderAggregate.Address>();

            CreateMap<CustomerBasketDto, CustomerBasket>();

            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<Order, OrderToReturneDto>()
                .ForMember(d => d.Deliverymethod, O => O.MapFrom(S => S.Deliverymethod.ShortName))
                .ForMember(d => d.Deliverymethodcost, O => O.MapFrom(S => S.Deliverymethod.Cost));

            CreateMap<OrderItem, OrderitemsDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(S => S.Proudect.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(S => S.Proudect.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(S => S.Proudect.PictureUrl));
                //.ForMember(d => d.PictureUrl, o => o.MapFrom<OrderPictureUrlResponse>());




        }
    }
}
