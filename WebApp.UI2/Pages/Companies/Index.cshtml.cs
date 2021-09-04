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
    public class IndexModel : PageModel
    {
        public string ApiUrl { get; }
        public IndexModel()
        {
            ApiUrl = ApiUrls.Rootlocal;
        }
        public class CompanyViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        [BindProperty]
        public IEnumerable<CompanyViewModel> Companies { get; set; }

        public async Task OnGet()
        {
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.Company.GetCompanies + "?PageNumber=1&PageSize=10&userName=" + User.Identity.Name);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<CompanyViewModel>>(resultuerinfo);
                Companies = data;
            }
          
        }

    }
}
