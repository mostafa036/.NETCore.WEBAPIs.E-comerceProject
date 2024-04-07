using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Entites.OrderAggregate;
using Talabat.Core.IRepositories;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Core.Specifications;

namespace Talabat.Sevices
{
    public class OrderServices : IOrderServices
    {
        private readonly IBasketRepository _basketRepository;
        private readonly Iunitofwork _unitofwork;
        private readonly IPaymentServices _paymentServices;

        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<Deliverymethod> _deliveryMethodRepo;
        //private readonly IGenericRepository<Order> _orderRepo;

        public OrderServices(
            IBasketRepository basketRepository,
            ////IGenericRepository<Product> ProductRepo,
            ////IGenericRepository<Deliverymethod> DeliveryMethodRepo,
            ////IGenericRepository<Order> orderRepo
            Iunitofwork unitofwork,
            IPaymentServices paymentServices
            )
        {
            _basketRepository = basketRepository;
            _unitofwork = unitofwork;
            _paymentServices = paymentServices;

            //    _productRepo = ProductRepo;
            //    _deliveryMethodRepo = DeliveryMethodRepo;
            //    _orderRepo = orderRepo;
        }


        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int DeliveryMethodId, Address ShipingAddress)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);

            var orderitems = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var product = await  _unitofwork.Repository<Product>().GetByIdAsync(item.Id);

                var ProductOtemOrder = new ProudectItemOrader(product.Id, product.Name, product.PictureUrl);            

                var orderitem = new OrderItem(ProductOtemOrder ,product.Price ,item.Quantity);

                orderitems.Add(orderitem);
            } 

            var Subtotal = orderitems.Sum(item  => item.Price*item.Quantity);

            var deliveryMethod = await _unitofwork.Repository<Deliverymethod>().GetByIdAsync(DeliveryMethodId);

            var spec = new OrderByPaymentintenIdSprcification(basket.PaymentIntentId);

            var exsitingorder = await _unitofwork.Repository<Order>().GetIdwithSpecAsync(spec);

            if (exsitingorder != null )
            {
                _unitofwork.Repository<Order>().Delete(exsitingorder);
                await _paymentServices.CreateOrUpdatePaymentinten(basketId);
            };

            var order = new Order(buyerEmail, ShipingAddress, deliveryMethod, orderitems, Subtotal ,basket.PaymentIntentId);

            await _unitofwork.Repository<Order>().CreateAsync(order);
            var result =  await _unitofwork.Complete();
            if (result < 0) return null;


            return order;
        }


        public async Task<IReadOnlyList<Deliverymethod>> Getdeliverymethodasunc()
        {
            var delivery = await _unitofwork.Repository<Deliverymethod>().GetAllAsync();
            return delivery;

        }

        public async Task<Order> Getorderbyid(int orderid, string buyerEmail)
        {
            var spec = new Orderwithitemanddeliveryspecfication(orderid, buyerEmail);
            var orders = await _unitofwork.Repository<Order>().GetIdwithSpecAsync(spec);
            return orders;


        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            var spec = new Orderwithitemanddeliveryspecfication(buyerEmail);
            var orders = await _unitofwork.Repository<Order>().GetallwithSpecAsync(spec);
            return orders;
        }
    }
}
