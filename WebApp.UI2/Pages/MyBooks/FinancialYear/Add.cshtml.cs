using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.UI2.Pages.MyBooks.FinancialYear
{
    public class AddModel : PageModel
    {
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            public string Name { get; set; }
        }
        public void OnGet()
        {
        }
    }
}
