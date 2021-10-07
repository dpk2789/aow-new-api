using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApp.UI2.Pages.Payment
{
    public class PayUMoneyModel : PageModel
    {
        private static HttpContext _httpContextAccessor;

        public PayUMoneyModel(HttpContext httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty] public PayUModel Input { get; set; }
        public class PayUModel
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string Amount { get; set; }
            [Required]
            public string Phone { get; set; }
            [Required]
            public string ProductInfo { get; set; }
            public string Email { get; set; }
        }
        public void OnGet()
        {
            string surl = "https://localhost:44320/Payment/SuccessPayment";
            string furl = "https://localhost:44320/Payment/FailurePayment";
            ViewData["surl"] = surl;
            ViewData["furl"] = furl;
        }

      

    }
}
