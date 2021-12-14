using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using WebApp.UI2.Helpers;
using Newtonsoft.Json;

namespace WebApp.RazorPages.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }
        public class ForgetPasswordResponse
        {
            public bool? IsEmailConfirmed { get; set; }
            public string UserId { get; set; }
            public string Msg { get; set; }
            public List<string> ErrorMessages { get; set; }
            public bool Success { get; set; }
            public string Code { get; set; }
        }
        public IActionResult OnGet()
        {

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            if (ModelState.IsValid)
            {
                if (!ModelState.IsValid) return Page();
                using var client = new HttpClient();
                var u = new Uri(ApiUrls.Identity.ForgetPassword);

                var json = JsonConvert.SerializeObject(new { Input.Email });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = await client.PostAsync(u, content);
                //postTask.Wait();
                var result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var data = JsonConvert.DeserializeObject<ForgetPasswordResponse>(result);

                if (data == null || data.IsEmailConfirmed == false)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }


                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", data.Code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
