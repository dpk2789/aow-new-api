using Aow.Infrastructure.Paging;
using Aow.Services.JournalEntry;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class VouchersController : ControllerBase
    {
        [HttpGet("api/Vouchers/GetVouchers")]
        public IActionResult GetVouchers([FromQuery] PagingParameters pagingParameters, Guid cmpId, [FromServices] GetJournalEntries getFinancialYears)
        {
            return Ok(getFinancialYears.Do(pagingParameters, cmpId));
        }

        [HttpGet("api/Vouchers/GetVoucher")]
        public IActionResult GetVoucher([FromQuery] Guid id, [FromServices] GetJournalEntry getProduct)
        {
            var result = getProduct.Do(id);
            return Ok(result);
        }

        [HttpPost("api/Vouchers/AddVoucher")]
        public async Task<IActionResult> AddVoucher([FromBody] AddVoucher.AddJournalEntryVoucherRequest request, [FromServices] AddVoucher addFinancialYear)
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
