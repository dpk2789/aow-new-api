using Aow.Infrastructure.Paging;
using Aow.Services.SundryItem;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApp.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class SundryItemController : ControllerBase
    {
        [HttpGet("api/SundryItem/GetSundryItems")]
        public IActionResult GetSundryItems([FromQuery] PagingParameters pagingParameters, Guid cmpId, [FromServices] GetSundryItems getFinancialYears)
        {
            return Ok(getFinancialYears.Do(pagingParameters, cmpId));
        }
    }
}
