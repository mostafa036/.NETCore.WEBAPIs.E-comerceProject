using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Core.Specifications
{
    public class Orderwithitemanddeliveryspecfication :BaseSpecifications<Order>
    {
        public Orderwithitemanddeliveryspecfication(string buyerEmail)
            : base(O =>O.Buyeremail == buyerEmail)
        {
            Includes.Add(O => O.Deliverymethod);
            Includes.Add(O => O.Items);

            AddOrderByDescending(O => O.OrderDate);
        }
        public Orderwithitemanddeliveryspecfication(int orderid, string buyerEmail)
           : base(O => O.Buyeremail == buyerEmail && O.Id == orderid)
        {
            Includes.Add(O => O.Deliverymethod);
            Includes.Add(O => O.Items);

            
        }
    }
}
