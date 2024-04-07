using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.APIs.Dtos;
using Talabat.Core.Entites;
using Talabat.Core.IRepositories;

namespace Talabat.APIs.Controllers
{
    public class BasketController : BaseAPIsController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasketId(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            //var updateUndcreateNasket = _mapper.Map<CustomerBasketDto,CustomerBasket>(basket);
            var updatedOrdeleted = await _basketRepository.UpdateBasketAsync(basket);
            return Ok(updatedOrdeleted);
        }

        [HttpDelete]
        public async Task Deletebasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
