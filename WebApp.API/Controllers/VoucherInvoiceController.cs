using Aow.Services.VoucherInvoice;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class VoucherInvoiceController : ControllerBase
    {
        [HttpGet("api/VoucherInvoice/GetVoucherInvoice")]
        public IActionResult GetVoucherInvoice([FromQuery] Guid id, [FromServices] GetVoucherInvoice getVoucher)
        {
            var result = getVoucher.Do(id);
            return Ok(result);
        }

        [HttpPost("api/VoucherInvoice/AddVoucherInvoice")]
        public async Task<ActionResult> Add([FromBody] AddVoucherWithItems.AddVoucherInvoiceRequest request, [FromServices] AddVoucherWithItems addFinancialYear)
        {
            if (ModelState.IsValid)
            {
                var response = await addFinancialYear.Do(request);

                if (!response.Success)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            return BadRequest();
        }

       
    }
}
