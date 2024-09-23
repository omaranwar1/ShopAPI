using System.Collections.Generic;
using System;
using System.Text;
using System.Threading.Tasks;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync(string searchTerm, int pageNumber, int pageSize, string sortBy, string sortDirection);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
    Task UpdateProductQuantityAsync(int productId, int quantity);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
}