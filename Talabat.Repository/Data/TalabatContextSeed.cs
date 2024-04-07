using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.OrderAggregate;

namespace Talabat.Repository.Data
{
    public class TalabatContextSeed
    {
        public static async Task SeedAsync(TalabatContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                    var brands =JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                    foreach (var brand in brands)
                        context.Set<ProductBrand>().Add(brand); // added
       
                }
                if (!context.ProductTypes.Any())
                {
                    var TypesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                    foreach (var type in types)
                        context.Set<ProductType>().Add(type); // added

                }
                if (!context.Products.Any())
                {
                    var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                    foreach (var product in products)
                        context.Set<Product>().Add(product); // added

                }

                if (!context.Deliverymethods.Any())
                {
                    var DeliveryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                    var deliverymethods = JsonSerializer.Deserialize<List<Deliverymethod>>(DeliveryData);

                    foreach (var deliverymethod in deliverymethods)
                        context.Set<Deliverymethod>().Add(deliverymethod); // added

                }

                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                
                var logger = loggerFactory.CreateLogger<TalabatContextSeed>();
                logger.LogError(ex, ex.Message);


            }
        }
    }
}


