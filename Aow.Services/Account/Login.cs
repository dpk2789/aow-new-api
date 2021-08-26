using Aow.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Aow.Services.Account
{
    [Service]
    public class Login
    {
        private readonly UserManager<AppUser> _userManager;
        public Login(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public class LoginRequest
        {
            [EmailAddress]
            public string Email { get; set; }
            public string Password { get; set; }          
        }
        public class LoginResponse
        {
            public string Token { get; set; }
            public bool Success { get; set; }
            public int expires_in { get; set; }
            public IEnumerable<string> ErrorMessages { get; set; }
        }
        public async Task<LoginResponse> Do(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new LoginResponse
                {
                    ErrorMessages = new[] { "User does not exist" }
                };

            };

            var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!checkPassword)
            {
                return new LoginResponse
                {
                    ErrorMessages = new[] { "Not Valid Password" }
                };
            }

            return await GenerateTokenForUserAuthResult(user);         

        }

        private async Task<LoginResponse> GenerateTokenForUserAuthResult(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            var key = Encoding.ASCII.GetBytes("aaaaaaaaabbbbbbbbbbbbbbbbbbbbbbbbbbb");
            var claims = new List<Claim>();
            foreach (var userClaim in userClaims)
            {
                claims.Add(new Claim(userClaim.Type, userClaim.Value));
            };
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, user.Id));
            var appIdentity = new ClaimsIdentity(claims);

            //var newTest = new ClaimsIdentity(new Claim[]
            //       {
            //        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //        new Claim(JwtRegisteredClaimNames.Email, user.Email),
            //        new Claim("Id", user.Id),
            //       });
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = appIdentity,
                Expires = DateTime.UtcNow.AddHours(5),
                Issuer = "abc123456789",
                Audience = "abc123456789",
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var Securitytoken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var tokenstring = new JwtSecurityTokenHandler().WriteToken(Securitytoken);
            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenstring);
            var claim = token.Claims.FirstOrDefault(c => c.Type == "sub").Value;

            //token.Header.Add("kid", "");
            //token.Payload.Remove("iss");
            //token.Payload.Add("iss", "your issuer");

            var tokenString = tokenHandler.WriteToken(Securitytoken);

            return new LoginResponse
            {
                Success = true,
                Token = tokenString,
                expires_in = 5
            };
        }
    }
}
