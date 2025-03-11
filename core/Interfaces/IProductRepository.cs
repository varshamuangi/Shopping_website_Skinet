using System;
using core.Entities;

namespace core.Interfaces;

public interface IProductRepository
{
   Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort);
   Task<Product?> GetProductByIdAsync(int id);
   Task<IReadOnlyList<string>> GetBrandsAsync();
   Task<IReadOnlyList<string>> GetTypesAsync();
   void AddProduct(Product product);
   void UpdateProduct(Product product);
   void DeleteProduct(Product product);
   bool ProductExits(int id);
   Task<bool> SaveChangesAsync();

}
