using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApp.UI2.Helpers;
using WebApp.UI2.Models;

namespace WebApp.UI2.Pages.MyBooks.FinancialYear
{
    public class AddModel : PageModel
    {
        public string ApiUrl { get; }
        public AddModel()
        {
            ApiUrl = ApiUrls.Rootlocal;
        }
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            public string Name { get; set; }
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();
            using var client = new HttpClient();
            var addProductsUri = new Uri(ApiUrls.Company.Create);


            var json = JsonConvert.SerializeObject(new { Input.Name });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var userAccessToken = User.Claims.FirstOrDefault(x => x.Type == "AcessToken")?.Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var postTask = await client.PostAsync(addProductsUri, content);
            //postTask.Wait();
            var result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<ApiResponse>(result);
            return RedirectToPage("Index");
        }
    }
}
