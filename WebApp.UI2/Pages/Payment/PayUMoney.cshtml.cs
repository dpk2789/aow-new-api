using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApp.UI2.Pages.Payment
{
    public class PayUMoneyModel : PageModel
    {
        public string RazorPayKey { get; }
        public string RazorPaySecret { get; }
        public PayUMoneyModel()
        {            
            RazorPayKey = "rzp_live_ovIIdD5BdthHdu";
            RazorPaySecret = "HefGk8cKCQ0ztGQbbcvBuy8d";
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
            public string Message { get; set; }
        }
        public IActionResult OnGet()
        {
            string surl = "https://localhost:44320/Payment/SuccessPayment";
            string furl = "https://localhost:44320/Payment/FailurePayment";          
            ViewData["furl"] = furl;
            ViewData["surl"] = surl;          
            return Page();
        }

      

    }
}
