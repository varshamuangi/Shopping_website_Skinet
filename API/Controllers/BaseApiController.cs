using API.RequestHelpers;
using core.Entities;
using core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected async Task<ActionResult> CreatedPagedResult<T>(IGenericRepository<T> repo,ISpecification<T> spec, 
                     int PageIndex,int PageSize)where T : BaseEntity
               {
                 var items = await repo.ListAsync(spec);
                 var count = await repo.CountAsync(spec);
                 var pagination = new Pagination<T>(PageIndex,PageSize,count,items);
                 return Ok(pagination);
               }
    }
}
