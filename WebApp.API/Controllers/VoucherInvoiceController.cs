using Aow.Services.VoucherInvoice;
using Aow.Services.VoucherItemVarient;
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
        public async Task<IActionResult> GetVoucherInvoice([FromQuery] Guid id, [FromServices] GetVoucherInvoice getVoucher)
        {
            var result = await getVoucher.Do(id);
            return Ok(result);
        }

        [HttpGet("api/VoucherInvoice/GetVoucherItem")]
        public async Task<IActionResult> GetVoucherItem([FromQuery] Guid id, [FromServices] GetVoucherItem getVoucher)
        {
            var result = await getVoucher.Do(id);
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

        [HttpPost("api/VoucherInvoice/AddVoucherItemVarient")]
        public async Task<ActionResult> AddVoucherItemVarient([FromBody] AddVoucherItemVarient.AddVoucherItemRequest request, [FromServices] AddVoucherItemVarient addVarient)
        {
            if (ModelState.IsValid)
            {
                var response = await addVarient.Do(request);

                if (!response.Success)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost("api/VoucherInvoice/UpdateVoucherInvoice")]
        public async Task<ActionResult> UpdateVoucher([FromBody] UpdateVoucherWithItems.UpdateVoucherInvoiceRequest request, [FromServices] UpdateVoucherWithItems updateVoucherWithItems)
        {
            if (ModelState.IsValid)
            {
                var response = await updateVoucherWithItems.Do(request);

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
