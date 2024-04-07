using System.Collections.Generic;

namespace Talabat.APIs.Dtos
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public List<BasketItemDto> itemDtos { get; set; } = new List<BasketItemDto>();

    }
}
