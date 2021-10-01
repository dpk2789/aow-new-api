using Aow.Services.VoucherWithItems;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class VoucherInvoiceController : ControllerBase
    {
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
