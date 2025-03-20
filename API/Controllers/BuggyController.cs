using API.DTOs;
using core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/buggy")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized(){
            return Unauthorized();
        }

        [HttpGet("badRequest")]
        public IActionResult GetBadRequest(){
            return BadRequest("Not a good request");
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFound(){
            return NotFound();
        }

        [HttpGet("internalerror")]
        public IActionResult GetInternalError(){
            throw new Exception("This is a test exception");
        }

        [HttpPost("validationerror")]
        public IActionResult GetValidationError(CreateProductDTO product){
            return Ok();
        }
    }
}
