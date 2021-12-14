using Aow.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Aow.Services.Account
{
    [Service]
    public class ForgetPassword
    {
        private readonly UserManager<AppUser> _userManager;
        public ForgetPassword(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public class ForgetPasswordRequest
        {
            [EmailAddress]
            public string Email { get; set; }

        }
        public class ForgetPasswordResponse
        {
            public bool? IsEmailConfirmed { get; set; }
            public string UserId { get; set; }
            public string Code { get; set; }
            public List<string> ErrorMessages { get; set; }
            public bool Success { get; set; }
        }

        public async Task<ForgetPasswordResponse> Do(ForgetPasswordRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser == null)
            {
                return new ForgetPasswordResponse
                {
                    ErrorMessages = { "Not User Allready" },
                    Success = false
                };
            };

            var user = new AppUser { UserName = request.Email, Email = request.Email };

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));


            return (new ForgetPasswordResponse
            {
                UserId = user.Id,
                Code = code,
                Success = true,
                IsEmailConfirmed = existingUser.EmailConfirmed
            });

        }
    }
}
