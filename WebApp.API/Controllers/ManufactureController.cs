using Aow.Infrastructure.Paging;
using Aow.Services.Manufacture;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ManufactureController : ControllerBase
    {
        [HttpGet("api/Manufacture/GetManufactureVouchers")]
        public IActionResult GetManufactureVouchers([FromQuery] PagingParameters pagingParameters, Guid fyrId, [FromServices] GetManufactureVouchers getVouchers)
        {
            return Ok(getVouchers.Do(pagingParameters, fyrId));
        }

        [HttpPost("api/Manufacture/AddManufactureVoucher")]
        public async Task<IActionResult> AddManufactureVoucher([FromBody] AddManufacture.AddManufactureRequest request, [FromServices] AddManufacture addFinancialYear)
        {
            var response = await addFinancialYear.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }
    }
}
