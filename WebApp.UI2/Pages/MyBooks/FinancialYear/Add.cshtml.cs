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
        private readonly ICookieHelper _cookieHelper;
        public string ApiUrl { get; }
        public AddModel(ICookieHelper cookieHelper)
        {
            ApiUrl = ApiUrls.Rootlocal;
            _cookieHelper = cookieHelper;
        }
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            public string Name { get; set; }
            public DateTime? Start { get; set; }
            public DateTime? End { get; set; }
            public bool IsActive { get; set; }
            public bool IsLocked { get; set; }          
            public string CompanyName { get; set; }
            public string CompanyId { get; set; }
        }
        public IActionResult OnGet()
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return RedirectToPage("/");
            }
            InputModel inputModel = new InputModel();
            inputModel.CompanyId = cmpid;
            Input = inputModel;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();
            using var client = new HttpClient();
            var addProductsUri = new Uri(ApiUrls.FinancialYear.Create);


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
