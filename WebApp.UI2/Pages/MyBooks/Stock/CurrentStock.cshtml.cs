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

namespace WebApp.UI2.Pages.MyBooks.Stock
{
    public class CurrentStockModel : PageModel
    {
        public class CurrentStockViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string ModelNumber { get; set; }
            public string Discription { get; set; }
            public string Size { get; set; }
            public decimal? SalePrice { get; set; }
            public string ItemType { get; set; }
            public string TaxType { get; set; }
            public bool Is_Taxable { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ItemAmount { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public Guid? VoucherItemId { get; set; }
           
        }
        public async Task<IActionResult> OnGet(Guid id)
        {
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.ProductVarients.GetProductVarients + "?PageNumber=1&PageSize=10&productId=" + id);
            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<CurrentStockViewModel>>(resultuerinfo);               
                ViewData["ProductId"] = id;
            }
            return Page();
        }

    }
}
