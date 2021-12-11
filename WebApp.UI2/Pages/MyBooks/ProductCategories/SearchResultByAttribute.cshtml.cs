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

namespace WebApp.UI2.Pages.MyBooks.ProductCategories
{
    public class SearchResultByAttributeModel : PageModel
    {
        public string ApiUrl { get; }
        private readonly ICookieHelper _cookieHelper;
        public SearchResultByAttributeModel(ICookieHelper cookieHelper)
        {
            ApiUrl = ApiUrls.Rootlocal;
            _cookieHelper = cookieHelper;
        }
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            public string Name { get; set; }
            public string CompanyId { get; set; }
            public string CategoryId { get; set; }           
        }
      
        public async Task<IActionResult> OnGet(Guid id)
        {
            using var client = new HttpClient();
            var updateProductsUri = new Uri(ApiUrls.ProductCategories.GetProductCategory + "?id=" + id);

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
