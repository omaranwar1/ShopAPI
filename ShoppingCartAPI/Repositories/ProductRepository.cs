using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Product>> GetProductsAsync(string searchTerm = "", int pageNumber = 1, int pageSize = 6, string sortBy = "name", string sortDirection = "asc")
    {
        searchTerm = searchTerm.ToLower();

        var productsQuery = from product in _context.Products
                            where string.IsNullOrEmpty(searchTerm) || product.Name.ToLower().StartsWith(searchTerm)
                            select product;

        switch (sortBy.ToLower())
        {
            case "price":
                productsQuery = sortDirection.ToLower() == "desc"
                    ? productsQuery.OrderByDescending(p => p.Price)
                    : productsQuery.OrderBy(p => p.Price);
                break;
            default:
                productsQuery = sortDirection.ToLower() == "desc"
                    ? productsQuery.OrderByDescending(p => p.Name)
                    : productsQuery.OrderBy(p => p.Name);
                break;
        }

        productsQuery = productsQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize);

      
        return await productsQuery.ToListAsync();
    }


    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        Product? product = await _context.Products.FindAsync(id);
        return product!;
    }

    public async Task AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductQuantityAsync(int productId, int quantity)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product != null)
        {
            product.TotalQuantity = product.TotalQuantity - quantity;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}