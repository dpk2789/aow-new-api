using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.Companies
{
    public class EditModel : PageModel
    {
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }          
            public string TaxNumber { get; set; }
            public string TaxType { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public string Currency { get; set; }
            public int? CurrencyId { get; set; }         
            public bool Success { get; set; }
            public string ShippingName { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string PlotNumber { get; set; }
            public string City { get; set; }
            public int? CityId { get; set; }
            public string State { get; set; }
            public int? StateId { get; set; }

            public string Country { get; set; }
            public int? CountryId { get; set; }
            public string PinCode { get; set; }

        }

        public async Task<IActionResult> OnGet(Guid Id)
        {
            using var client = new HttpClient();
            var updateProductsUri = new Uri(ApiUrls.Company.GetCompany + "?id=" + Id);

            var userAccessToken = User.Claims.FirstOrDefault(x => x.Type == "AcessToken").Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var postTask = await client.GetAsync(updateProductsUri);
            var result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            var data = JsonConvert.DeserializeObject<InputModel>(result);
            Input = data;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();
           
            using var client = new HttpClient();
            var addProductsUri = new Uri(ApiUrls.Product.Update);

            var json = JsonConvert.SerializeObject(new { Input.Id, Input.Name, Input.TaxNumber });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var userAccessToken = User.Claims.FirstOrDefault(x => x.Type == "AcessToken").Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var postTask = await client.PutAsync(addProductsUri, content);
            //postTask.Wait();
            var result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<InputModel>(result);
            Input = data;
            return RedirectToPage("Index");
        }
    }
}
