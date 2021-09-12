using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks.LedgerCategories
{
    public class AddModel : PageModel
    {
        private readonly ICookieHelper _cookieHelper;
        public string ApiUrl { get; }
        public AddModel(ICookieHelper cookieHelper)
        {
            ApiUrl = ApiUrls.Rootlocal;
            _cookieHelper = cookieHelper;
        }
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            public string Name { get; set; }
            public string CompanyName { get; set; }
            public string CompanyId { get; set; }
        }
        public IActionResult OnGet()
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return RedirectToPage("/");
            }
            InputModel inputModel = new InputModel();
            inputModel.CompanyId = cmpid;
            Input = inputModel;
            return Page();
        }
    }
}
