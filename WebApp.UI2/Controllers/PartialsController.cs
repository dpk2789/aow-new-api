using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Controllers
{
    public class PartialsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetFinancialYearsPartialView()
        {
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.FinancialYear.GetFinancialYears + "?PageNumber=1&PageSize=10");

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;          
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<IEnumerable<WebApp.UI2.Pages.MyBooks.FinancialYear.IndexModel.FinancialYearViewModel>>(resultuerinfo);
            return PartialView("_FyrListPartial", data);
        }
    }
}
