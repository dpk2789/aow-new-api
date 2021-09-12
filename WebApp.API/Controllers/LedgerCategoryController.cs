using Aow.Infrastructure.Paging;
using Aow.Services.LedgerCategory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    public class LedgerCategoryController : ControllerBase
    {
        [HttpGet("api/LedgerCategory/GetLedgerCategories")]
        public IActionResult GetLedgerCategories([FromQuery] PagingParameters pagingParameters, Guid cmpId, [FromServices] GetLedgerCategories getFinancialYears)
        {
            return Ok(getFinancialYears.Do(pagingParameters, cmpId));
        }

        [HttpGet("api/LedgerCategory/GetLedgerCategory")]
        public IActionResult GetLedgerCategory([FromQuery] Guid id, [FromServices] GetLedgerCategory getProduct)
        {
            var result = getProduct.Do(id);
            return Ok(result);
        }

        [HttpPost("api/LedgerCategory/AddLedgerCategory")]
        public async Task<IActionResult> AddLedgerCategory([FromBody] AddLedgerCategory.AddLedgerCategoryRequest request, [FromServices] AddLedgerCategory addFinancialYear)
        {
            var response = await addFinancialYear.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }

        [HttpPut("api/LedgerCategory/UpdateLedgerCategory")]
        public async Task<IActionResult> UpdateLedgerCategory([FromBody] UpdateLedgerCategory.UpdateLedgerCategoryRequest request, [FromServices] UpdateLedgerCategory updateFinancialYear)
        {
            var response = await updateFinancialYear.Do(request);

            if (response == null)
            {
                return BadRequest("Failed to update");
            }
            return Ok(response);
        }

        [HttpDelete("api/LedgerCategory/DeleteLedgerCategory")]
        public async Task<IActionResult> DeleteLedgerCategory(Guid id, [FromServices] DeleteLedgerCategory deleteFinancialYear)
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
