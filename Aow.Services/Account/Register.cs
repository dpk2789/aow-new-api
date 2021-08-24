using System.ComponentModel.DataAnnotations;

namespace Aow.Services.Account
{
    [Service]
    public class Register
    {
        public class UserRegisterRequest
        {
            [EmailAddress]
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }
    }
}
