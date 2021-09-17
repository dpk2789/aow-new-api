using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApp.UI2.Helpers;
using WebApp.UI2.Models;

namespace WebApp.UI2.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly ICookieHelper _cookieHelper;
        public UserAccountController(ICookieHelper cookieHelper)
        {
            _cookieHelper = cookieHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // await HttpContext.SignOutAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            foreach (var key in HttpContext.Request.Cookies.Keys)
            {
                HttpContext.Response.Cookies.Append(key, "", new CookieOptions() { Expires = DateTime.Now.AddDays(-1) });
            }

            return RedirectToPage("/Index");
        }

        public JsonResult SetFinancialYear(string fyrId)
        {
            _cookieHelper.Set("fYrCookee", fyrId.ToString(), 60);           
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Success = true;
            apiResponse.Msg = "Successfully Set";
            return Json(apiResponse);
        }
    }
}
