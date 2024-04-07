using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites.Order_Aggregate
{
    public class OrderItem :BaseEntity
    {
        public OrderItem() 
        {
        
        }
        public OrderItem(ProudectItemOrader proudect, decimal price, int quantity)
        {
            Proudect = proudect;
            Price = price;
            Quantity = quantity;
        }
        public ProudectItemOrader Proudect { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set;}
    }
}
