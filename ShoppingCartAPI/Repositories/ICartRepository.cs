using System.Threading.Tasks;
using System.Collections.Generic;
public interface ICartRepository
{
    Task<Cart> GetCartByIdAsync(int cartId);
    Task AddItemToCartAsync(int cartId, CartItem item);
    Task RemoveItemFromCartAsync(int cartId, int itemId);
    Task UpdateItemQuantityAsync(int cartId, int itemId, int quantity);
    Task ClearCartAsync(int cartId);
}