using core.Entities;
using core.Interfaces;
using infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController(IProductRepository repo) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(string? brand, string? type,string? sort)
        {
            return Ok(await repo.GetProductsAsync(brand,type,sort));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await repo.GetProductByIdAsync(id);
            if(product == null)
            {
                return NotFound();
            }
           return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
           
            repo.AddProduct(product);
            if(await repo.SaveChangesAsync()){
                 return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            

            return BadRequest("Can't create this product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id,Product product)
        {
           if(product.Id != id || !ProductExits(id))
           {
             return BadRequest("Cannot update this product");
           }

           repo.UpdateProduct(product);
           if(await repo.SaveChangesAsync())
           {
              return NoContent();
           }
           return BadRequest("Problem Updating the Product");
        }

        [HttpDelete("{id:int}")]   
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetProductByIdAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            repo.DeleteProduct(product);
            if(await repo.SaveChangesAsync())
            {
                return NoContent();
            }
            return BadRequest("Problem Deleting the Product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok(await repo.GetBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok(await repo.GetTypesAsync());
        }
        private bool ProductExits(int id)
        {
            return repo.ProductExits(id);
        }
    }
}
