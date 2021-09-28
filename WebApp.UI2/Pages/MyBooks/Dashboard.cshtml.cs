using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks
{
    public class DashboardModel : PageModel
    {
        private readonly ICookieHelper _cookieHelper;     
        public DashboardModel(ICookieHelper cookieHelper)
        {          
            _cookieHelper = cookieHelper;
        }
        public void OnGet(Guid companyId)
        {
            _cookieHelper.Set("cmpCookee", companyId.ToString(), 60);
            var fyrId = _cookieHelper.Get("fYrCookee");
            if (!string.IsNullOrEmpty(fyrId))
            {
                _cookieHelper.Remove("fYrCookee");
            }
        }
    }
}
