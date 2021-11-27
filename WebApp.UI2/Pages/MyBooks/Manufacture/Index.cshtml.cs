using Microsoft.AspNetCore.Http;
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

namespace WebApp.UI2.Pages.MyBooks.Manufacture
{
    public class IndexModel : PageModel
    {
        public string ApiUrl { get; }
        private readonly ICookieHelper _cookieHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IndexModel(ICookieHelper cookieHelper, IHttpContextAccessor httpContextAccessor)
        {
            ApiUrl = ApiUrls.Rootlocal;
            _cookieHelper = cookieHelper;
            _httpContextAccessor = httpContextAccessor;
        }
        public class GetManufactureVouchersViewModel
        {
            public Guid Id { get; set; }
            public DateTime? Date { get; set; }
            public string VoucherNumber { get; set; }
            public string VoucherName { get; set; }
            public string RefId { get; set; }
            public string Note { get; set; }
            public bool? Type { get; set; }
            public decimal? Total { get; set; }
            public List<ManufacturingVarientsViewModel> ManufacturingVarients { get; set; }
        }
        public class ManufacturingVarientsViewModel
        {
            public Guid Id { get; set; }
            public string VairentName { get; set; }
            public decimal? Quantity { get; set; }
            public string Type { get; set; }
            public Guid? StockProductVariantId { get; set; }
            public Guid ManufactureId { get; set; }

        }
        [BindProperty]
        public IEnumerable<GetManufactureVouchersViewModel> ManufacturingVouchers { get; set; }
        public async Task<IActionResult> OnGet()
        {
            string currentUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" +
               _httpContextAccessor.HttpContext.Request.Host.Value + _httpContextAccessor.HttpContext.Request.Path.Value;

            var fYrId = _cookieHelper.Get("fYrCookee");
            var cmpid = _cookieHelper.Get("cmpCookee");
            if (string.IsNullOrEmpty(fYrId) && string.IsNullOrEmpty(fYrId))
            {
                return Redirect("/MyBooks/FinancialYear/SetFinancialYear?returnUrl=" + currentUrl);
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.ManufactureVoucher.GetManufactureVouchers + "?PageNumber=1&PageSize=100&fyrId=" + fYrId);
            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<GetManufactureVouchersViewModel>>(resultuerinfo);
                ManufacturingVouchers = data;
            }
            return Page();
        }
    }
}
