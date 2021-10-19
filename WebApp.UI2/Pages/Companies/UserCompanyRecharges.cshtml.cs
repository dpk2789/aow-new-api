using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.Companies
{
    public class UserCompanyRechargesModel : PageModel
    {
        public class UserCompanyRechargeViewModel
        {
            public string RazorpayPaymentId { get; set; }
            public string RazorpayOrderId { get; set; }
            public string RazorpaySignature { get; set; }
            public string RazorReference { get; set; }
            public string SessionId { get; set; }
            public string UserId { get; set; }
            public Guid CompanyId { get; set; }
            public string Email { get; set; }
            public string ContactNo { get; set; }
            public decimal Amount { get; set; }
            public string upi { get; set; }
            public string rrnNo { get; set; }
            public string status { get; set; }
            public string AddressLine1 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public DateTime StartDateUtc { get; set; }
            public DateTime EndDateUtc { get; set; }
            public string Currency { get; set; }
            public string Receipt { get; set; }
            public string Offerid { get; set; }
            public string Attempts { get; set; }
            public string Notes { get; set; }
            public string CreatedAt { get; set; }
            public int NoOfDays { get; set; }
        }

        [BindProperty]
        public IEnumerable<UserCompanyRechargeViewModel> CompanyRecharges { get; set; }
        public async Task<IActionResult> OnGet(Guid Id)
        {
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.Payment.GetUserPayments + "?userName=" + User.Identity.Name + "&cmpId=" + Id);

            var userAccessToken = User.Claims.FirstOrDefault(x => x.Type == "AcessToken").Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var postTask = await client.GetAsync(getProductsUri);
            var result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (result != null)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<UserCompanyRechargeViewModel>>(result);
                CompanyRecharges = data;
            }
            return Page();
        }
    }
}

