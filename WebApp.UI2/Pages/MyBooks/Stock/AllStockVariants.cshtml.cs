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

namespace WebApp.UI2.Pages.MyBooks.Stock
{
    public class AllStockVariantsModel : PageModel
    {
        public string ApiUrl { get; }
        private readonly ICookieHelper _cookieHelper;
        public AllStockVariantsModel(ICookieHelper cookieHelper)
        {
            ApiUrl = ApiUrls.Rootlocal;
            _cookieHelper = cookieHelper;
        }
        public class AllStockVariantsViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
            public string VoucherNumber { get; set; }
            public string ModelNumber { get; set; }
            public string Size { get; set; }
            public string UniqueNumber { get; set; }
            public string Status { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ConsumedQuantity { get; set; }
            public decimal? ItemAmount { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public Guid? VoucherItemId { get; set; }
        }

        [BindProperty]
        public IEnumerable<AllStockVariantsViewModel> CurrentStockVariants { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return RedirectToPage("/");
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.StockVarients.GetAllStoreVarientsByCompany + "?PageNumber=1&PageSize=100&companyId=" + cmpid);
            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<AllStockVariantsViewModel>>(resultuerinfo);
                CurrentStockVariants = data;
            }
            return Page();
        }
    }
}
