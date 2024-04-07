using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.OrderAggregate;
using Talabat.Core.IRepositories;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Product = Talabat.Core.Entites.Product;

namespace Talabat.Sevices
{
    public class PaymentinServices : IPaymentServices
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly Iunitofwork _iunitofwork;

        public PaymentinServices(
            IConfiguration configuration,
            IBasketRepository basketRepository,
            Iunitofwork iunitofwork)
        {
            
            _configuration = configuration;
            _basketRepository = basketRepository;
            _iunitofwork = iunitofwork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentinten(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];

            var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket == null) return null;

            var shippingPrice = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliverymethod = await _iunitofwork.Repository<Deliverymethod>().GetByIdAsync(basket.DeliveryMethodId.Value);

                shippingPrice = deliverymethod.Cost;

                basket.ShippingPrice = deliverymethod.Cost;
            }

            foreach (var item in basket.Items)
            {
                var product = await _iunitofwork.Repository<Product>().GetByIdAsync(item.Id);

                if (item.Price != product.Price)
                    item.Price = product.Price;

            }

            var services = new PaymentIntentService();

            PaymentIntent intent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {

                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long) basket.Items.Sum(item => item.Quantity * item.Price * 100) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card"}
                };

                intent = await services.CreateAsync(options);

                basket.PaymentIntentId = intent.Id;

                basket.ClientSecret = intent.ClientSecret;

            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Quantity * item.Price * 100) + (long)shippingPrice * 100,

                };
                await services.UpdateAsync(basket.PaymentIntentId, options);
            }
            await _basketRepository.UpdateBasketAsync(basket);
            return basket;
        }
    }
}
