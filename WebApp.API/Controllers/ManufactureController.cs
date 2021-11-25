using Aow.Services.Manufacture;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ManufactureController : ControllerBase
    {

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
