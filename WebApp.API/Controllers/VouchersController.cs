using Aow.Infrastructure.Paging;
using Aow.Services.Voucher;
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
        public IActionResult GetVouchers([FromQuery] PagingParameters pagingParameters, string voucherName, Guid fyrId, [FromServices] GetVouchers getVouchers)
        {
            return Ok(getVouchers.Do(pagingParameters, voucherName, fyrId));
        }

        [HttpGet("api/Vouchers/GetVoucher")]
        public IActionResult GetVoucher([FromQuery] Guid id, [FromServices] GetVoucher getVoucher)
        {
            var result = getVoucher.Do(id);
            return Ok(result);
        }

        [HttpPost("api/Vouchers/AddVoucher")]
        public async Task<IActionResult> AddVoucher([FromBody] AddVoucher.AddVoucherRequest request, [FromServices] AddVoucher addFinancialYear)
        {
            var response = await addFinancialYear.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }

        [HttpDelete("api/Vouchers/DeleteVoucher")]
        public async Task<IActionResult> DeleteVoucher(Guid id, [FromServices] DeleteVoucher deleteFinancialYear)
        {
            var result = await deleteFinancialYear.Do(id);
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
