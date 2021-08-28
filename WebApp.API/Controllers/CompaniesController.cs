using Aow.Infrastructure.Paging;
using Aow.Services.Companies;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("api/Company/GetCompany")]
        public IActionResult GetCompany(Guid id, [FromServices] GetCompany getProduct)
        {
            return Ok(getProduct.Do(id));
        }

        [HttpPost("api/Company/CreateCompany")]
        public async Task<IActionResult> CreateCompany([FromBody] AddCompany.CreateRequest request, [FromServices] AddCompany addCompany)
        {
            var response = await addCompany.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }

        [HttpPut("api/Company/UpdateCompany")]
        public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompany.UpdateCompanyRequest request, [FromServices] UpdateCompany updateCompany)
        {
            var response = await updateCompany.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }


        [HttpDelete("api/Company/DeleteCompany")]
        public async Task<IActionResult> DeleteCompany(Guid id, [FromServices] DeleteCompany deleteProduct)
        {
            var result = await deleteProduct.Do(id);
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
