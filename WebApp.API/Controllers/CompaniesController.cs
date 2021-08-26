using Aow.Infrastructure.Paging;
using Aow.Services.Companies;
using Microsoft.AspNetCore.Mvc;

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
    }
}
