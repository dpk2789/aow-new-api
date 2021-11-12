using Aow.Infrastructure.Paging;
using Aow.Services.Store;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApp.API.Controllers
{
    //  [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        [HttpGet("api/Stocks/GetStocks")]
        public IActionResult GetStocks([FromQuery] PagingParameters pagingParameters, Guid productId, [FromServices] GetCurrentStock getProductVariants)
        {
            var result = getProductVariants.Do(pagingParameters, productId);
            return Ok(result);
        }
    }
}
