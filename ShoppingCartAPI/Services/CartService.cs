using System.Linq;
using System.Threading.Tasks;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public async Task<Cart> GetCartAsync(int cartId)
    {
        return await _cartRepository.GetCartByIdAsync(cartId);
    }

    public async Task AddProductToCartAsync(int cartId, int productId, int quantity, decimal price, string name, string imageURL)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        if (product != null)
        {
            var cartItem = new CartItem { ProductId = productId, Quantity = quantity , Price = price, Name = name, ImageURL = imageURL};
            await _cartRepository.AddItemToCartAsync(cartId, cartItem);
        }
    }

    public async Task RemoveProductFromCartAsync(int cartId, int productId)
    {
        var cart = await _cartRepository.GetCartByIdAsync(cartId);
        var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
        if (cartItem != null)
        {
            await _cartRepository.RemoveItemFromCartAsync(cartId, cartItem.CartId);
        }
    }

    public async Task UpdateCartItemQuantityAsync(int cartId, int itemId, int quantity)
    {
        await _cartRepository.UpdateItemQuantityAsync(cartId, itemId, quantity);
    }

    public async Task ClearCartAsync(int cartId)
    {
        var cart = await _cartRepository.GetCartByIdAsync(cartId);
        if (cart != null)
        {
            foreach (var item in cart.CartItems)
            {
                await _productRepository.UpdateProductQuantityAsync(item.ProductId, item.Quantity);
            }

            await _cartRepository.ClearCartAsync(cartId);
        }
    }
}