using Aow.Infrastructure.Paging;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApp.API.Controllers
{
    //  [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        [HttpGet("api/ProductVarients/GetProductVarients")]
        public IActionResult GetStocks([FromQuery] PagingParameters pagingParameters, Guid productId, [FromServices] GetProductVariants getProductVariants)
        {
            var result = getProductVariants.Do(pagingParameters, productId);
            return Ok(result);
        }
    }
}
