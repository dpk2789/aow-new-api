using Aow.Services.Account;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Aow.Services.Account.Register;

namespace WebApp.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("api/user/register")]
        public async Task<IActionResult> register([FromBody] UserRegisterRequest request, [FromServices] Register register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await register.Do(request);
            return Ok(result);

        }
        [HttpGet("api/user/ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code, [FromServices] ConfirmEmail confirmEmail)
        {
            if (userId == null || code == null)
            {
                return BadRequest("/");
            }
            var result = await confirmEmail.Do(userId, code);
            return Ok(result);
        }
    }
}
