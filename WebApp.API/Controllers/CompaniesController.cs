using Aow.Infrastructure.Paging;
using Aow.Services.Companies;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        [HttpGet("api/Companies/GetCompanies")]
        public IActionResult GetCompanies([FromQuery] PagingParameters pagingParameters, [FromServices] GetCompanies getCompanies)
        {
            return Ok(getCompanies.Do(pagingParameters));
        }

        [HttpPost("api/Order/CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] AddCompany.CreateRequest request, [FromServices] AddCompany addCompany)
        {
           
            var success = await addCompany.Do(request);

            if (success.Success)
            {
                return Ok("Company Created");
            }

            return BadRequest("Failed to add to cart");
        }
    }
}
