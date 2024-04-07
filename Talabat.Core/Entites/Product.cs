using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites
{
    public class Product : BaseEntity
    {
        public string Name  { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        //[ForeignKey("ProductBrand")]
        public int ProductBrandId { get; set; } // int : Not Allow Null
        public ProductBrand ProductBrand { get; set; } // Navigational Property [ONE]
        //[ForeignKey("ProductType")]
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; } // Navigational Property [ONE]

    }
}
