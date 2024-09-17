using Microsoft.EntityFrameworkCore;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(string searchTerm)
    {
        return await _productRepository.GetProductsAsync(searchTerm);  // Execute the query and get the results
    }


    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _productRepository.GetAllProductsAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetProductByIdAsync(id);
    }

    public async Task AddProductAsync(Product product)
    {
        // Add any business logic here, like validation or additional checks
        await _productRepository.AddProductAsync(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        // Business logic for updating the product
        await _productRepository.UpdateProductAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        // Business logic for deleting the product
        await _productRepository.DeleteProductAsync(id);
    }

    public async Task UpdateProductQuantityAsync(int id, int quantity)
    {
        // Business logic for deleting the product
        await _productRepository.UpdateProductQuantityAsync(id,quantity);
    }


}