public interface IProductService
{
    Task<IEnumerable<Product>> GetProductsAsync(string searchTerm);
    Task<IEnumerable<Product>> GetProductsAsync();            // Retrieve all products
    Task<Product> GetProductByIdAsync(int id);                // Retrieve a specific product by ID
    Task AddProductAsync(Product product);                    // Add a new product
    Task UpdateProductAsync(Product product);                 // Update an existing product
    Task DeleteProductAsync(int id);                          // Delete a product by ID
    Task UpdateProductQuantityAsync(int id, int quantity);
}