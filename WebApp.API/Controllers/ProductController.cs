using Aow.Infrastructure.Paging;
using Aow.Services.Products;
using Microsoft.AspNetCore.Mvc;
using System;


namespace WebApp.API.Controllers
{
    //  [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductController()
        {

        }

        [HttpGet("api/Products/GetProducts")]
        public IActionResult GetProduct([FromQuery] PagingParameters pagingParameters, [FromServices] GetProducts getProduct)
        {
            return Ok(getProduct.Do(pagingParameters));
        }

        [HttpGet("api/Products/GetProductById")]
        public IActionResult GetProductById(Guid Id, [FromServices] GetProduct getProduct)
        {
            var product = getProduct.Do(Id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


    }
}
