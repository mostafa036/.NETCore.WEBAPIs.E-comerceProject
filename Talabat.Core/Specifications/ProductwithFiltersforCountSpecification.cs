using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
    public class ProductwithFiltersforCountSpecification : BaseSpecifications<Product>
    {
        public ProductwithFiltersforCountSpecification(ProduectSpecParams produectParams)
        :base( P =>
            (string.IsNullOrEmpty(produectParams.Search) || P.Name.ToLower().Contains(produectParams.Search)) &&
            (!produectParams.BrandId.HasValue || P.ProductBrandId == produectParams.BrandId.Value) &&
            (!produectParams.TypeId.HasValue || P.ProductTypeId == produectParams.TypeId.Value))
        {

        }
    }
}
