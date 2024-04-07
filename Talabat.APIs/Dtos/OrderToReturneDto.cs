using System.Collections.Generic;
using System;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Entites.OrderAggregate;

namespace Talabat.APIs.Dtos
{
    public class OrderToReturneDto
    {
        public int  Id { get; set; }
        public string Buyeremail { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public string Status { get; set; }

        public Address Shipingaddress { get; set; }

        // public Deliverymethod Deliverymethod { get; set; }
        public string Deliverymethod { get; set; }
        public decimal Deliverymethodcost { get; set; }
        public ICollection<OrderItem> Items { get; set; }

        public decimal Subtotal { get; set; }

        public string PaymentintendId { get; set; }

        public decimal Total { get; set; }
    }
}
