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

namespace WebApp.UI2.Pages.MyBooks.ProductCategoryAttributes
{
    public class UpdateModel : PageModel
    {
        public class UpdateProductsAttributeViewModel
        {
            public Guid Id { get; set; }
            public Guid ProductCategoryId { get; set; }
            public string Name { get; set; }
            public string ProductName { get; set; }
            public string OptionValue { get; set; }
            public IList<AttributesOptions> AttributesOptions { get; set; }
        }

        public class AttributesOptions
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; }
        }

        [BindProperty] public UpdateProductsAttributeViewModel Input { get; set; }
        public async Task<IActionResult> OnGet(Guid Id)
        {
          
            using var client = new HttpClient();
            var updateProductsUri = new Uri(ApiUrls.Product.GetProduct + "?id=" + Id);

            var userAccessToken = User.Claims.FirstOrDefault(x => x.Type == "AcessToken").Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var postTask = await client.GetAsync(updateProductsUri);
            var result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (result != null)
            {
                var data = JsonConvert.DeserializeObject<UpdateProductsAttributeViewModel>(result);
                Input = data;
            }
           
            return Page();
        }
    }
}
