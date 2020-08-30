using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory) 
        {
            try 
            {
                if(!context.ProductBrands.Any()) 
                {
                    string brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                    IReadOnlyList<ProductBrand> brands = JsonSerializer.Deserialize<IReadOnlyList<ProductBrand>>(brandsData);

                    foreach(ProductBrand brand in brands) 
                    {
                        context.ProductBrands.Add(brand);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.ProductTypes.Any()) 
                {
                    string brandsTypes = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    IReadOnlyList<ProductType> types = JsonSerializer.Deserialize<IReadOnlyList<ProductType>>(brandsTypes);

                    foreach(ProductType type in types) 
                    {
                        context.ProductTypes.Add(type);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.Products.Any()) 
                {
                    string products = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    IReadOnlyList<Product> productsEntities = JsonSerializer.Deserialize<IReadOnlyList<Product>>(products);

                    foreach(Product product in productsEntities) 
                    {
                        context.Products.Add(product);
                    }

                    await context.SaveChangesAsync();
                }
            }  
            catch  (Exception ex)   
            {
                ILogger logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}