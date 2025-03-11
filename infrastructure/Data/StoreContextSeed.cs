using System;
using core.Entities;
using System.Text.Json;
namespace infrastructure.Data;

public class StoreContextSeed
{
   public static async Task SeedAsync(StoreContext context)
{
    if (!context.Products.Any())
    {
        //var productsData = await File.ReadAllTextAsync("../Data/SeedData/products.json");
        var productsData = await File.ReadAllTextAsync("C:/Users/varsh/OneDrive/Documents/Project_Git/skinet/INFRASTRUCTURE/Data/SeedData/products.json");

        var products = JsonSerializer.Deserialize<List<Product>>(productsData);
        if (products == null)
        {
            return;
        }

        context.Products.AddRange(products);
        await context.SaveChangesAsync();

    }
}
}
