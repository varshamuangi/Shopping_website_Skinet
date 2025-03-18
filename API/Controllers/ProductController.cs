using core.Entities;
using core.Interfaces;
using core.Specifications;
using infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController(IGenericRepository<Product> repo) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(string? brand, string? type,string? sort)
        {
            var spec = new ProductSpecification(brand,type,sort);
            var products = await repo.ListAsync(spec);
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await repo.GetIdByAsync(id);
            if(product == null)
            {
                return NotFound();
            }
           return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
           
            repo.Add(product);
            if(await repo.SaveAllAsync()){
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

           repo.Update(product);
           if(await repo.SaveAllAsync())
           {
              return NoContent();
           }
           return BadRequest("Problem Updating the Product");
        }

        [HttpDelete("{id:int}")]   
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetIdByAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            repo.Remove(product);
            if(await repo.SaveAllAsync())
            {
                return NoContent();
            }
            return BadRequest("Problem Deleting the Product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpecification();
            //Todo
            return Ok(await repo.ListAsync(spec));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();
            //Todo
            return Ok(await repo.ListAsync(spec));
        }
        private bool ProductExits(int id)
        {
            return repo.Exists(id);
        }
    }
}
