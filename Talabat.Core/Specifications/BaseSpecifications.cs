using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>(); // best performance
        public Expression<Func<T, object>> Orderby { get; set; }
        public Expression<Func<T, object>> OrderbyDescending { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationpageEnable { get; set; }

        public BaseSpecifications() 
        {
            // Inclides = new List<Expression<Func<T, bool>>>();
        }
        public BaseSpecifications(Expression<Func<T, bool>> criteria) // P => P.id == 10 // for item = id
        {
            Criteria = criteria;
            // Inclides = new List<Expression<Func<T, bool>>>();
        }
        public void AddOrderby(Expression<Func<T, object>> orderbyExpression)
        {
            Orderby = orderbyExpression;
        }
        public void AddOrderByDescending(Expression<Func<T, object>> orderbyDescendingExpression)
        {
            OrderbyDescending = orderbyDescendingExpression;
        }
        public void ApplyPagination(int skip , int take)
        {
            IsPaginationpageEnable = true;
            Skip = skip;
            Take = take;
        }


    }
}
