using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.UI2.Helpers;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace WebApp.UI2.Pages.MyBooks.AttributeOptions
{
    public class EditModel : PageModel
    {      
        public class AttributesOption
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public Guid AttributeId { get; set; }
            public bool IsChecked { get; set; }
        }

        [BindProperty] public AttributesOption Input { get; set; }
        public async Task<IActionResult> OnGet(Guid Id) 
        {
            using var client = new HttpClient();
            var updateProductsUri = new Uri(ApiUrls.ProductAttributeOption.GetProductAttributeOption + "?id=" + Id);
            var userAccessToken = User.Claims.FirstOrDefault(x => x.Type == "AcessToken").Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var postTask = await client.GetAsync(updateProductsUri);
            var result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (result != null)
            {
                var data = JsonConvert.DeserializeObject<AttributesOption>(result);
                Input = data;
            }

            return Page();
        }
    }
}
