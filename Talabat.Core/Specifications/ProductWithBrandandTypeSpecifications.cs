using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandandTypeSpecifications : BaseSpecifications<Product>
    {
        // This is Constructor is used for Get All Products
        public ProductWithBrandandTypeSpecifications(ProduectSpecParams produectParams)
            :base(P =>
                (string.IsNullOrEmpty(produectParams.Search) || P.Name.ToLower().Contains(produectParams.Search))&& 
                (!produectParams.BrandId.HasValue || P.ProductBrandId == produectParams.BrandId.Value)  &&
                (!produectParams.TypeId.HasValue || P.ProductTypeId == produectParams.TypeId.Value)
        )  
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);

            ApplyPagination(produectParams.PageSize * (produectParams.PageIndex - 1), produectParams.PageSize);

            if (!string.IsNullOrEmpty(produectParams.Sort))
            {
                switch (produectParams.Sort) 
                {
                    case "priceAsc":
                        AddOrderby(P=> P.Price); 
                        break;
                    case "priceDesc":
                        AddOrderByDescending(P=> P.Price); 
                        break;
                    default: AddOrderby(P=> P.Name);
                        break;
                }
            }
        }
        // This is Constructor is used for Get  Products by id
        public ProductWithBrandandTypeSpecifications(int id):base( P => P.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }

    }
}
