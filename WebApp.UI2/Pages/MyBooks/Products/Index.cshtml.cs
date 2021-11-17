using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks.Products
{
    public class IndexModel : PageModel
    {
        public string ApiUrl { get; }
        private readonly ICookieHelper _cookieHelper;
        public IndexModel(ICookieHelper cookieHelper)
        {
            ApiUrl = ApiUrls.Rootlocal;
            _cookieHelper = cookieHelper;
        }

        public class ProductViewModel
        {
            public Guid Id { get; set; }
            public Guid? ProductCategoryId { get; set; }
            public Guid? ProductId { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public string ModelNumber { get; set; }
            public string Title { get; set; }
            public string Percent { get; set; }
            public string ProductTaxCode { get; set; }
            public string DiscountType { get; set; } //dynamic amount or %
            public string ItemType { get; set; }
            public string TaxType { get; set; }
            public string ItemTypeId { get; set; }
            public string CategoryName { get; set; }
            public string AutoGenerateName { get; set; }
            public decimal? SalePrice { get; set; }
        }
        [BindProperty]
        public IEnumerable<ProductViewModel> Products { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return RedirectToPage("/");
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.Product.GetProducts + "?PageNumber=1&PageSize=100&cmpId=" + cmpid);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<ProductViewModel>>(resultuerinfo);
                Products = data;
            }
            return Page();
        }
    }
}
