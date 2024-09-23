using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCartApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCartApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(
          [FromQuery] string searchTerm = "",
          [FromQuery] int pageNumber = 1, // Default page number is 1
          [FromQuery] int pageSize = 6,  // Default page size is 10
          [FromQuery] string sortBy = "name",  // Default sort by 'name'
          [FromQuery] string sortDirection = "asc" // Default sort direction is ascending
           )
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var products = await _productService.GetProductsAsync(searchTerm, pageNumber, pageSize, sortBy, sortDirection);

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        //// GET: api/product
        //[HttpGet]
        //public async Task<IEnumerable<Product>> GetProducts([FromQuery] string searchTerm = "")
        //{
        //    var products = await _productService.GetProductsAsync(searchTerm);
        //    return products;
        //}

 
        // GET: api/product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        // PUT: api/product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            try
            {
                await _productService.UpdateProductAsync(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _productService.GetProductByIdAsync(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductAsync(id);
            return NoContent();
        }


        [HttpPut("{id}/update-quantity")]
        public async Task<IActionResult> UpdateProductQuantity(int id, int quantity)
        {
            await _productService.UpdateProductQuantityAsync(id, quantity);
            return NoContent();
        }
    }
}
