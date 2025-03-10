using core.Entities;
using infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController(StoreContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await context.Products.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await context.Products.FindAsync(id);
            if(product == null)
            {
                return NotFound();
            }
           return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
           context.Products.Add(product);
           await context.SaveChangesAsync();
           return product;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id,Product product)
        {
           if(product.Id != id || !ProductExits(id))
           {
             return BadRequest("Cannot update this product");
           }

           context.Entry(product).State = EntityState.Modified;
           await context.SaveChangesAsync();
           return NoContent();
        }

        [HttpDelete("{id:int}")]   
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            if(product == null)
            {
                return NotFound();
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private bool ProductExits(int id)
        {
            return context.Products.Any(x =>x.Id == id);
        }
    }
}
