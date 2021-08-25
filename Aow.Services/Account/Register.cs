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
    public class Register
    {
        private readonly UserManager<AppUser> _userManager;
        public Register(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public class UserRegisterRequest
        {
            [EmailAddress]
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }
        public class UserRegisterResponse
        {
            public string UserId { get; set; }
            public string Code { get; set; }
            public List<string> ErrorMessages { get; set; }
            public bool Success { get; set; }
        }
        public async Task<UserRegisterResponse> Do(UserRegisterRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new UserRegisterResponse
                {
                    ErrorMessages = { "User Allready Exist" },
                    Success = false
                };

            };

            var user = new AppUser { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                UserRegisterResponse userRegisterResponse = new UserRegisterResponse();
                foreach (var error in result.Errors)
                {
                    userRegisterResponse.ErrorMessages.Add(error.Description);
                }
                userRegisterResponse.Success = false;
                return userRegisterResponse;
            }
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));


            return (new UserRegisterResponse
            {
                UserId = user.Id,
                Code = code,
                Success = true
            });

        }


    }
}
