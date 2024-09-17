using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;

    public CartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Cart> GetCartByIdAsync(int cartId)
    {
        return await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.CartId == cartId)
            ?? throw new Exception("Cart not found.");
    }

    public async Task AddItemToCartAsync(int cartId, CartItem item)
    {
        var cart = await GetCartByIdAsync(cartId);
        cart.CartItems.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveItemFromCartAsync(int cartId, int itemId)
    {
        var cart = await GetCartByIdAsync(cartId);
        var item = cart.CartItems.FirstOrDefault(i => i.CartId == itemId);
        if (item != null)
        {
            cart.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateItemQuantityAsync(int cartId, int itemId, int quantity)
    {
        var cart = await GetCartByIdAsync(cartId);
        var item = cart.CartItems.FirstOrDefault(i => i.CartItemId == itemId);
        if (item != null)
        {
            item.Quantity = quantity;
            await _context.SaveChangesAsync();
        }
    }

    public async Task ClearCartAsync(int cartId)
    {
        var cart = await GetCartByIdAsync(cartId);
        cart.CartItems.Clear();
        await _context.SaveChangesAsync();
    }
}
