using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;

namespace Talabat.Repository.Data
{
    public class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery (IQueryable<TEntity> Input_query , ISpecifications<TEntity> Spec) 
        {
            var query = Input_query;

            if ( Spec.Criteria != null ) // P => P.Id == 10
               query =  query.Where( Spec.Criteria );

            if ( Spec.IsPaginationpageEnable)
                query = query.Skip(Spec.Skip).Take(Spec.Take);

            if ( Spec.Orderby != null )
                query = query.OrderBy( Spec.Orderby );

            if(Spec.OrderbyDescending != null )
                query = query.OrderByDescending( Spec.OrderbyDescending );


            query = Spec.Includes.Aggregate(query, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));

            return query;
        }
    }
}
