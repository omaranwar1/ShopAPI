using Microsoft.EntityFrameworkCore;
using ShoppingCartApp.Models;

public class AppDbContext : DbContext
{

    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}