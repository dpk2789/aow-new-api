using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks.Manufacture
{
    public class IndexModel : PageModel
    {
        public string ApiUrl { get; }
        private readonly ICookieHelper _cookieHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IndexModel(ICookieHelper cookieHelper, IHttpContextAccessor httpContextAccessor)
        {
            ApiUrl = ApiUrls.Rootlocal;
            _cookieHelper = cookieHelper;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult OnGet()
        {
            string currentUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" +
               _httpContextAccessor.HttpContext.Request.Host.Value + _httpContextAccessor.HttpContext.Request.Path.Value;
           
            string fYrId = _cookieHelper.Get("fYrCookee");
            if (string.IsNullOrEmpty(fYrId) && string.IsNullOrEmpty(fYrId))
            {
                return Redirect("/MyBooks/FinancialYear/SetFinancialYear?returnUrl=" + currentUrl);
            }
            return Page();
        }
    }
}
