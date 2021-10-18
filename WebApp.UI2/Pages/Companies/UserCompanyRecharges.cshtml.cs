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
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        [BindProperty]
        public IEnumerable<UserCompanyRechargeViewModel> CompanyRecharges { get; set; }
        public async Task<IActionResult> OnGet(Guid Id)
        {
            using var client = new HttpClient();
            var updateProductsUri = new Uri(ApiUrls.Company.GetCompany + "?id=" + Id);

            var userAccessToken = User.Claims.FirstOrDefault(x => x.Type == "AcessToken").Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var postTask = await client.GetAsync(updateProductsUri);
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

