using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCartApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCartApp.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/cart/5
        [HttpGet("{cartId}")]
        public async Task<ActionResult<Cart>> GetCart(int cartId)
        {
            var cart = await _cartService.GetCartAsync(cartId);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // POST: api/cart/{cartId}/add/{productId}
        [HttpPost("{cartId}/add/{productId}")]
        public async Task<IActionResult> AddProductToCart(int cartId, int productId, [FromBody] AddProductToCartDto addProductToCartDto)
        {
            // Extract quantity and price from the DTO
            int quantity = addProductToCartDto.Quantity;
            decimal price = addProductToCartDto.Price;
            string name = addProductToCartDto.Name;
            string imageURL = addProductToCartDto.ImageURL;

            // Update your service to handle the price
            await _cartService.AddProductToCartAsync(cartId, productId, quantity, price,name,imageURL);
            return Ok(); // You can customize the response as needed
        }

        // DELETE: api/cart/{cartId}/remove/{productId}
        [HttpDelete("{cartId}/remove/{productId}")]
        public async Task<IActionResult> RemoveProductFromCart(int cartId, int productId)
        {
            await _cartService.RemoveProductFromCartAsync(cartId, productId);
            return Ok();
        }

        // PUT: api/cart/{cartId}/update/{itemId}
        [HttpPut("{cartId}/update/{itemId}")]
        public async Task<IActionResult> UpdateCartItemQuantity(int cartId, int itemId, [FromBody] int quantity)
        {
            await _cartService.UpdateCartItemQuantityAsync(cartId, itemId, quantity);
            return NoContent();
        }

        // DELETE: api/cart/{cartId}/clear
        [HttpDelete("{cartId}/clear")]
        public async Task<IActionResult> ClearCart(int cartId)
        {
            await _cartService.ClearCartAsync(cartId);
            return NoContent();
        }
    }
}


public class AddProductToCartDto
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public required string ImageURL { get; set; }
    public required string Name { get; set; }
}