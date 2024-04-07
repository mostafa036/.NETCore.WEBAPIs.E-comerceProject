using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Entites.OrderAggregate;
using Address = Talabat.Core.Entites.OrderAggregate.Address;

namespace Talabat.Core.Entites.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public Order() { }
        public Order(string buyeremail, Address shipingaddress, Deliverymethod deliverymethod,ICollection <OrderItem> items, decimal subtotal , string paymentintenId)
        {
            Buyeremail = buyeremail;
            Shipingaddress = shipingaddress;
            Deliverymethod = deliverymethod;
            Items = items;
            Subtotal = subtotal;
            PaymentintendId = paymentintenId;
        }

        public string Buyeremail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address Shipingaddress { get; set; }
        public Deliverymethod Deliverymethod { get; set; }
        public ICollection<OrderItem> Items { get; set;}
        public decimal Subtotal { get; set; }
        public string PaymentintendId { get; set; }
        public decimal Gettotal()
        =>Subtotal + Deliverymethod.Cost;
    }
}
