using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{

    public class BuggyController : BaseAPIsController
    {
        private readonly TalabatContext context;

        public BuggyController(TalabatContext context)
        {
            this.context = context;
        }

        [HttpGet("NotFound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = context.Products.Find(100);
            if(product == null) return NotFound(new ApiResponese(404,"moooot"));
            return Ok(product);
        }

        [HttpGet("ServerError")]
        public ActionResult GetErrorRequest() 
        {
            var product = context.Products.Find(100);
            var producttoreturn = product.ToString();
            return Ok(producttoreturn);
        }

        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponese(400, "Bad request"));
        }

        [HttpGet("BadRequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
        return Ok();
        }

    }
}
