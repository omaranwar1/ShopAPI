using System.Threading.Tasks;

public interface ICartService
{
    Task<Cart> GetCartAsync(int cartId);
    Task AddProductToCartAsync(int cartId, int productId, int quantity, decimal price, string name, string imageURL);
    Task RemoveProductFromCartAsync(int cartId, int productId);
    Task UpdateCartItemQuantityAsync(int cartId, int itemId, int quantity);
    Task ClearCartAsync(int cartId);
}