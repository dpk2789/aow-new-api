using Aow.Infrastructure.Paging;
using Aow.Services.JournalEntry;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class JournalEntriesController : ControllerBase
    {
        [HttpGet("api/JournalEntries/GetJournalEntries")]
        public IActionResult GetJournalEntries([FromQuery] PagingParameters pagingParameters, Guid cmpId, [FromServices] GetJournalEntries getFinancialYears)
        {
            return Ok(getFinancialYears.Do(pagingParameters, cmpId));
        }

        [HttpGet("api/JournalEntries/GetJournalEntry")]
        public IActionResult GetJournalEntry([FromQuery] Guid id, [FromServices] GetJournalEntry getProduct)
        {
            var result = getProduct.Do(id);
            return Ok(result);
        }

        [HttpPost("api/JournalEntries/AddJournalEntries")]
        public async Task<IActionResult> AddJournalEntries([FromBody] AddJournalEntries.AddJournalEntryVoucherRequest request, [FromServices] AddJournalEntries addFinancialYear)
        {
            var response = await addFinancialYear.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }

        [HttpPut("api/JournalEntries/UpdateJournalEntries")]
        public async Task<IActionResult> UpdateJournalEntries([FromBody] UpdateJournalEntries.UpdateJournalEntriesRequest request, [FromServices] UpdateJournalEntries updateFinancialYear)
        {
            var response = await updateFinancialYear.Do(request);

            if (response == null)
            {
                return BadRequest("Failed to update");
            }
            return Ok(response);
        }
    }
}
