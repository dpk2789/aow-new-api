using Aow.Services.StoreVarient;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class StockVarientsController : ControllerBase
    {
        [HttpGet("api/StockVarients/GetAllStoreVarientsByCompany")]
        public IActionResult GetAllStoreVarientsByCompany(Guid companyId, [FromServices] GetAllStoreVarients getProductVariants)
        {
            var result = getProductVariants.Do(companyId);
            return Ok(result);
        }

        [HttpGet("api/StockVarients/GetStockVariant")]
        public IActionResult GetStockVariant([FromQuery] Guid id, [FromServices] GetStockVariant getStock)
        {
            var result = getStock.Do(id);
            return Ok(result);
        }

        [HttpPut("api/StockVarients/UpdateStockVariant")]
        public async Task<IActionResult> UpdateStockVariant([FromBody] UpdateStockVariant.UpdateStockVariantRequest request, [FromServices] UpdateStockVariant updateFinancialYear)
        {
            var response = await updateFinancialYear.Do(request);

            if (response == null)
            {
                return BadRequest("Failed to update");
            }
            return Ok(response);
        }
    }
}
