using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks.Stock
{
    public class UpdateStockVariantModel : PageModel
    {
        public string ApiUrl { get; }
        private readonly ICookieHelper _cookieHelper;
        public UpdateStockVariantModel(ICookieHelper cookieHelper)
        {
            ApiUrl = ApiUrls.Rootlocal;
            _cookieHelper = cookieHelper;
        }
        public class UpdateStockVariantViewModel
        {
            public Guid Id { get; set; }
            public Guid? VoucherItemVarientId { get; set; }
            public int? SrNo { get; set; }
            public string UniqueNumber { get; set; }
            public string Status { get; set; }
            public string ItemName { get; set; }
            public string Description { get; set; }
            public decimal? SalePrice { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ConsumedQuantity { get; set; }
            public decimal? ItemAmount { get; set; }
            public decimal Rate { get; set; }
            public Guid? ProductVariantId { get; set; }
            public Guid StockId { get; set; }
        }
        [BindProperty] public UpdateStockVariantViewModel Input { get; set; }
        public async Task<IActionResult> OnGet(Guid Id)
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return RedirectToPage("/");
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.StockVarients.GetStockVariant + "?id=" + Id);
            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<UpdateStockVariantViewModel>(resultuerinfo);
                Input = data;
            }
            return Page();
        }
    }
}
