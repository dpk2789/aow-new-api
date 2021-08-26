using Aow.Services.Account;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Aow.Services.Account.ConfirmEmail;
using static Aow.Services.Account.Login;
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
        [HttpPost("api/user/ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest confirmEmailRequest, [FromServices] ConfirmEmail confirmEmail)
        {
            if (confirmEmailRequest.UserId == null || confirmEmailRequest.Code == null)
            {
                return BadRequest("/");
            }
            var result = await confirmEmail.Do(confirmEmailRequest.UserId, confirmEmailRequest.Code);
            return Ok(result);
        }

        [HttpPost("api/user/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, [FromServices] Login login)
        {
            var result = await login.Do(request);
            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
