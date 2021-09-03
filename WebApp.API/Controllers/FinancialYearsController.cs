using Aow.Infrastructure.Paging;
using Aow.Services.FinancialYear;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class FinancialYearsController : ControllerBase
    {
        [HttpGet("api/FinancialYear/GetFinancialYears")]
        public IActionResult GetFinancialYears([FromQuery] PagingParameters pagingParameters, [FromServices] GetFinancialYears getFinancialYears)
        {
            return Ok(getFinancialYears.Do(pagingParameters));
        }

        [HttpGet("api/FinancialYear/GetFinancialYear")]
        public IActionResult GetFinancialYear(Guid id, [FromServices] GetFinancialYear getProduct)
        {
            return Ok(getProduct.Do(id));
        }

        [HttpPost("api/FinancialYear/AddFinancialYear")]
        public async Task<IActionResult> AddFinancialYear([FromBody] AddFinancialYear.AddFinancialYearRequest request, [FromServices] AddFinancialYear addFinancialYear)
        {
            var response = await addFinancialYear.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }

        [HttpDelete("api/FinancialYear/DeleteFinancialYear")]
        public async Task<IActionResult> DeleteFinancialYear(Guid id, [FromServices] DeleteFinancialYear deleteFinancialYear)
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
