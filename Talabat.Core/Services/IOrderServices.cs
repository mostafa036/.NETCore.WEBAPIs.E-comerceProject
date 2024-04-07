using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Entites.OrderAggregate;
using Address = Talabat.Core.Entites.OrderAggregate.Address;

namespace Talabat.Core.Services
{
    public interface IOrderServices
    {
        Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int DeliveryMethodId,Address  ShipingAddress);
        Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail);
        Task<Order> Getorderbyid(int orderid, string buyerEmail);
        Task<IReadOnlyList<Deliverymethod>> Getdeliverymethodasunc();
    }
}
