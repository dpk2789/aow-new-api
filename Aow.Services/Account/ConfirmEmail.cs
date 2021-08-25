using Aow.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;

namespace Aow.Services.Account
{
    [Service]
    public class ConfirmEmail
    {
        private readonly UserManager<AppUser> _userManager;
        public ConfirmEmail(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public class ConfirmEmailResponse
        {
            public string UserId { get; set; }
            public string Code { get; set; }
            public string StatusMessage { get; set; }
            public bool Success { get; set; }
        }
        public async Task<ConfirmEmailResponse> Do(string userId, string code)
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                return (new ConfirmEmailResponse
                {
                    UserId = user.Id,
                    Code = code,
                    Success = false,
                    StatusMessage = "Error confirming your email"
                });
            }

            return (new ConfirmEmailResponse
            {
                UserId = user.Id,
                Code = code,
                Success = true,
                StatusMessage = "Thank you for confirming your email."
            });

        }
    }

}
