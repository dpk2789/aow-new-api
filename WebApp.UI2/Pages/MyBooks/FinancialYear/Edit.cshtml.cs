using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks.FinancialYear
{
    public class EditModel : PageModel
    {
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            public string Name { get; set; }
            public DateTime? Start { get; set; }
            public DateTime? End { get; set; }
            public bool IsActive { get; set; }          
            public string CompanyName { get; set; }
            public string CompanyId { get; set; }
        }
        public async Task<IActionResult> OnGet(Guid Id)
        {
            using var client = new HttpClient();
            var updateProductsUri = new Uri(ApiUrls.FinancialYear.GetFinancialYear + "?id=" + Id);

            var userAccessToken = User.Claims.FirstOrDefault(x => x.Type == "AcessToken").Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var postTask = await client.GetAsync(updateProductsUri);
            var result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (result != null)
            {
                var data = JsonConvert.DeserializeObject<InputModel>(result);
                Input = data;
            }

            return Page();
        }
    }
}
