using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites.OrderAggregate
{
    public class Deliverymethod :BaseEntity
    {
        public Deliverymethod() 
        {
        
        }
        public Deliverymethod(string shortName, decimal cost, string description, string deliveryTime)
        {
            ShortName = shortName;
            Cost = cost;
            Description = description;
            DeliveryTime = deliveryTime;
        }

        public string ShortName { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }

    }
}
