using Aow.Infrastructure.Paging;
using Aow.Services.UserPayment;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpGet("api/Payment/GetUserPayments")]
        public async Task<IActionResult> GetUserPayments([FromQuery] PagingParameters pagingParameters, string userName, [FromServices] GetUserPayments getPayments)
        {
            var result = await getPayments.Do(pagingParameters, userName);
            return Ok(result);
        }

        [HttpPost("api/Payment/AddUserPayment")]
        public async Task<IActionResult> AddUserPayment([FromBody] AddUserPayment.AddPaymentRequest request, [FromServices] AddUserPayment addCompany)
        {
            var response = await addCompany.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }
    }
}
