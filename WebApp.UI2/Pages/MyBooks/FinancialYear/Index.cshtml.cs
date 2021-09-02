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

namespace WebApp.UI2.Pages.MyBooks.FinancialYear
{
    public class IndexModel : PageModel
    {
        public class FinancialYearViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public DateTime? Start { get; set; }
            public DateTime? End { get; set; }
            public bool IsActive { get; set; }
            public bool? IsLocked { get; set; }
        }

        [BindProperty]
        public IEnumerable<FinancialYearViewModel> FinancialYears { get; set; }
        public async Task OnGet()
        {
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.FinancialYear.GetFinancialYears + "?PageNumber=1&PageSize=10");

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<IEnumerable<FinancialYearViewModel>>(resultuerinfo);
            FinancialYears = data;
        }

    }
}
