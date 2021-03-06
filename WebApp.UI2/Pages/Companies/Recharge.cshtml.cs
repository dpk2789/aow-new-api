using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.Companies
{
    public class RechargeModel : PageModel
    {
        public string RazorPayKey { get; }
        public string RazorPaySecret { get; }
        public RechargeModel()
        {
            RazorPayKey = "rzp_live_ovIIdD5BdthHdu";
            RazorPaySecret = "HefGk8cKCQ0ztGQbbcvBuy8d";
        }
        [BindProperty] public RechargeViewModel Input { get; set; }
        public class RechargeViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string PANNumber { get; set; }
            public string Email { get; set; }
            public string UserId { get; set; }
            public string Mobile { get; set; }
            public int NoOfDays { get; set; }
        }

        public async Task<IActionResult> OnGet(Guid Id)
        {
            using var client = new HttpClient();
            var updateProductsUri = new Uri(ApiUrls.Company.GetCompany + "?id=" + Id);

            var userAccessToken = User.Claims.FirstOrDefault(x => x.Type == "AcessToken").Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var postTask = await client.GetAsync(updateProductsUri);
            var result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            var data = JsonConvert.DeserializeObject<RechargeViewModel>(result);
            Input = data;
            Input.Email = User.Identity.Name;
            return Page();
        }
    }
}
