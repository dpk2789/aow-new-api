using Aow.Infrastructure.Paging;
using Aow.Services.Ledger;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedgersController : ControllerBase
    {
        [HttpGet("api/Ledger/GetLedgers")]
        public IActionResult GetLedgers([FromQuery] PagingParameters pagingParameters, Guid cmpId, [FromServices] GetLedgers getFinancialYears)
        {
            return Ok(getFinancialYears.Do(pagingParameters, cmpId));
        }

        [HttpGet("api/Ledger/GetLedger")]
        public IActionResult GetLedger([FromQuery] Guid id, [FromServices] GetLedger getProduct)
        {
            var result = getProduct.Do(id);
            return Ok(result);
        }

        [HttpPost("api/Ledger/AddLedger")]
        public async Task<IActionResult> AddLedger([FromBody] AddLedger.AddLedgerRequest request, [FromServices] AddLedger addFinancialYear)
        {
            var response = await addFinancialYear.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }

        [HttpPut("api/Ledger/UpdateLedger")]
        public async Task<IActionResult> UpdateLedger([FromBody] UpdateLedger.UpdateLedgerRequest request, [FromServices] UpdateLedger updateFinancialYear)
        {
            var response = await updateFinancialYear.Do(request);

            if (response == null)
            {
                return BadRequest("Failed to update");
            }
            return Ok(response);
        }

        [HttpDelete("api/Ledger/DeleteLedger")]
        public async Task<IActionResult> DeleteLedger(Guid id, [FromServices] DeleteLedger deleteFinancialYear)
        {
            var result = await deleteFinancialYear.Do(id);
            if (!result.Success)
            {
                return BadRequest();
            }
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
