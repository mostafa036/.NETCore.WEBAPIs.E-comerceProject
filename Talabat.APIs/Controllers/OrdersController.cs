using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Entites.OrderAggregate;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
    [Authorize]
    public class OrdersController : BaseAPIsController
    {
        private readonly IOrderServices _orderServices;
        private readonly IMapper _mapper;

        public OrdersController(IOrderServices orderServices, IMapper mapper)
        {
            _orderServices = orderServices;
            _mapper = mapper;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]   
        public async Task<ActionResult<OrderToReturneDto>> CreateOrder(OrderDto orderDto)
        {
            var buyeremail = User.FindFirstValue(ClaimTypes.Email);

            var orderAddress = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);

            var order = await _orderServices.CreateOrderAsync(buyeremail, orderDto.BasketId, orderDto.DeliveryMethodId, orderAddress);

            if (order == null) return BadRequest(new ApiResponese(400));

            return Ok(_mapper.Map<Order , OrderToReturneDto>(order));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturneDto>>> Getorderforuser()
        {
            var buyeremail = User.FindFirstValue(ClaimTypes.Email);

            var orders = await _orderServices.GetOrderForUserAsync(buyeremail);
            
            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturneDto>> (orders));
        }

        [HttpGet("id")]
        public async Task<ActionResult<OrderToReturneDto>> Getorderforuser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var order = await _orderServices.Getorderbyid(id, buyerEmail);

            if (order == null) return BadRequest(new ApiResponese(40));

            return Ok(_mapper.Map<Order,OrderToReturneDto> (order));
        }

        [HttpGet("Delivery")] 
        public async Task<ActionResult<IReadOnlyList<Deliverymethod>>> GetDeliveryMethod()
            {

            var deliverymethod = await _orderServices.Getdeliverymethodasunc();

            return Ok(deliverymethod); 

            }
    }
}
