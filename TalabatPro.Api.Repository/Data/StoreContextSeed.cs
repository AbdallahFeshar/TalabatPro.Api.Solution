using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TalabatPro.Api.Core.Entities;

namespace TalabatPro.Api.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext dbContext)
        {


            if (dbContext.Brands.Count() == 0)
            {

                var BrandsData = File.ReadAllText("../TalabatPro.Api.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                if (brands?.Count() > 0)
                {
                    foreach (var brand in brands)
                    {
                        dbContext.Set<ProductBrand>().Add(brand);
                    }
                    await dbContext.SaveChangesAsync();

                }
            }


            if (dbContext.Categories.Count() == 0)
            {
                var categoriesData = File.ReadAllText("../TalabatPro.Api.Repository/Data/DataSeed/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

                if (categories?.Count() > 0)
                {
                    foreach (var category in categories)
                    {
                        dbContext.Set<ProductCategory>().Add(category);
                    }
                    await dbContext.SaveChangesAsync();

                }
            }


            if (dbContext.Products.Count() == 0)
            {
                var productsData = File.ReadAllText("../TalabatPro.Api.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        dbContext.Set<Product>().Add(product);
                    }
                    await dbContext.SaveChangesAsync();

                }
            }


        }
    }
}
