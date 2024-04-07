using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Core.Specifications
{
    public class OrderByPaymentintenIdSprcification :BaseSpecifications<Order>
    {
        public OrderByPaymentintenIdSprcification(string paymentIntenId )
            :base( o => o.PaymentintendId == paymentIntenId)
        {
            
        }
    }
}
