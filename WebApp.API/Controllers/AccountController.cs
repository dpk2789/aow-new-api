//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using static Aow.Services.Account.Register;

//namespace WebApp.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AccountController : ControllerBase
//    {
//        [HttpPost("api/user/register")]
//        public async Task<IActionResult> register([FromBody] UserRegisterRequest request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest();
//            }
//            if (ModelState.IsValid)
//            {
//                var user = new AppUser { UserName = request.Email, Email = request.Email };
//                var result = await _userManager.CreateAsync(user, request.Password);
//                if (result.Succeeded)
//                {
//                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
//                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));


//                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
//                    {
//                        return RedirectToPage("RegisterConfirmation", new { email = request.Email });
//                    }
//                    else
//                    {
//                        await _signInManager.SignInAsync(user, isPersistent: false);
//                        return Ok(new AuthRegiterResponse
//                        {
//                            UserId = user.Id,
//                            Msg = code,
//                            Success = true
//                        });
//                    }
//                }
//                List<string> ErrorMessages = new List<string>();

//                foreach (var error in result.Errors)
//                {
//                    ErrorMessages.Add(error.Description);
//                    ModelState.AddModelError(string.Empty, error.Description);
//                }
//                return BadRequest(new AuthRegiterResponse
//                {
//                    ErrorMessages = ErrorMessages,
//                    Success = false
//                });
//            }
//            var authResult = await _identityService.RegisterAsync(request.Email, request.Email);
//            if (!authResult.Success)
//            {
//                return BadRequest(new AuthRegiterResponse
//                {
//                    ErrorMessages = authResult.ErrorMessages.ToList(),
//                    Success = false
//                });
//            }
//            return Ok();
//        }
//    }
//}
