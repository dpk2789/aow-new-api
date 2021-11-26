using Aow.Services.StoreVarient;
using Microsoft.AspNetCore.Mvc;
using System;

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
    }
}
