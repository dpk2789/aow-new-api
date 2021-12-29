using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks
{
    public class DashboardModel : PageModel
    {
        private readonly ICookieHelper _cookieHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DashboardModel(ICookieHelper cookieHelper, IHttpContextAccessor httpContextAccessor)
        {
            _cookieHelper = cookieHelper;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult OnGet(Guid companyId)
        {
            _cookieHelper.Set("cmpCookee", companyId.ToString(), 60);
            var fyrId = _cookieHelper.Get("fYrCookee");
           
            string currentUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" +
             _httpContextAccessor.HttpContext.Request.Host.Value + _httpContextAccessor.HttpContext.Request.Path.Value;
          
            if (string.IsNullOrEmpty(fyrId) && string.IsNullOrEmpty(fyrId))
            {
                return Redirect("/MyBooks/FinancialYear/SetFinancialYear?returnUrl=" + currentUrl);
            }
            return Page();
        }
    }
}
