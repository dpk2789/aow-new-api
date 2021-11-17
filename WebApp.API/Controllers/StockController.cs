using Aow.Infrastructure.Paging;
using Aow.Services.Stock;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
    //  [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        [HttpGet("api/Stocks/GetStocks")]
        public IActionResult GetStocks([FromQuery] PagingParameters pagingParameters, Guid companyId, [FromServices] GetCurrentStock getProductVariants)
        {
            var result = getProductVariants.Do(pagingParameters, companyId);
            return Ok(result);
        }

        [HttpPost("api/Stocks/AddToStock")]
        public async Task<IActionResult> AddToStock([FromBody] AddToStock.AddToStockRequest request, [FromServices] AddToStock addFinancialYear)
        {
            var response = await addFinancialYear.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }

        [HttpDelete("api/Stocks/DeleteStock")]
        public async Task<IActionResult> DeleteStock(Guid id, [FromServices] DeleteStock deleteFinancialYear)
        {
            var result = await deleteFinancialYear.Do(id);           
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
