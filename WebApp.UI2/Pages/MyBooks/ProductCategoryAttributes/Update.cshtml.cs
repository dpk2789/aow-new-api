using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks.ProductCategoryAttributes
{
    public class UpdateModel : PageModel
    {
        public string ApiUrl { get; }
        private readonly ICookieHelper _cookieHelper;
        public UpdateModel(ICookieHelper cookieHelper)
        {
            ApiUrl = ApiUrls.Rootlocal;
            _cookieHelper = cookieHelper;
        }

        public class UpdateProductAttributeRequest
        {
            public Guid Id { get; set; }
            public Guid CategoryId { get; set; }
            public string Name { get; set; }
            public string OptionValue { get; set; }
            public IList<AttributesOptions> AttributesOptions { get; set; }
        }

        public class AttributesOptions
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public Guid AttributeId { get; set; }           
            public bool IsChecked { get; set; }
        }

        [BindProperty] public UpdateProductAttributeRequest Input { get; set; }
        public async Task<IActionResult> OnGet(string Id)
        {
            Guid id = Guid.Parse(Id);
            using var client = new HttpClient();
            var updateProductsUri = new Uri(ApiUrls.ProductAttributes.GetProductAttribute + "?id=" + Id);
            var userAccessToken = User.Claims.FirstOrDefault(x => x.Type == "AcessToken").Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var postTask = await client.GetAsync(updateProductsUri);
            var result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (result != null)
            {
                var data = JsonConvert.DeserializeObject<UpdateProductAttributeRequest>(result);
                Input = data;
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id, UpdateProductAttributeRequest request, string[] AttributeOptions)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                request.Name = Input.Name;
                request.Id = Input.Id;

                using var client = new HttpClient();
                Uri u = new Uri(ApiUrls.ProductAttributes.Update);
                //postTask.Wait();               
                var attributeOptionList = new List<AttributesOptions>();
                if (AttributeOptions != null)
                {
                    foreach (var option in AttributeOptions)
                    {
                        var attributeOption = new AttributesOptions();
                        attributeOption.Id = Guid.NewGuid();
                        attributeOption.AttributeId = request.Id;
                        attributeOption.Name = option;
                        attributeOptionList.Add(attributeOption);
                    }
                }
                request.AttributesOptions = attributeOptionList;
                var json = JsonConvert.SerializeObject(new { request.Id, request.Name, request.AttributesOptions });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                //HTTP POST
                var postTask = await client.PutAsync(u, content);
                string result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            var url = "https://localhost:44320/MyBooks/ProductCategoryAttributes/Update?id=" + request.Id;
            return RedirectToPage("/MyBooks/ProductCategoryAttributes/Update", new { Id = request.Id } );
        }
    }
}
