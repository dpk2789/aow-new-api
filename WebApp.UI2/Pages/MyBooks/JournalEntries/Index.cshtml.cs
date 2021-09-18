using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks.JournalEntries
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
        [BindProperty]
        public IEnumerable<VoucherViewModel> VoucherViewModelList { get; set; }
        public class VoucherViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }           
            public virtual IEnumerable<JournalEntryViewModel> JournalEntries { get; set; }
        }
        public class JournalEntryViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
        // public static string AppBaseUrl => $"{_httpContextAccessor.HttpContext.Request.Scheme}://{Current.Request.Host}{Current.Request.PathBase}";
        
        // public IEnumerable<JournalEntryViewModel> JournalEntries { get; set; }
        public async Task<IActionResult> OnGet(string voucherName)
        {
            string currentUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host.Value + _httpContextAccessor.HttpContext.Request.Path.Value;
            var fyrId = _cookieHelper.Get("fYrCookee");
            if (string.IsNullOrEmpty(fyrId) && string.IsNullOrEmpty(fyrId))
            {
                return Redirect("/MyBooks/FinancialYear/SetFinancialYear?returnUrl=" + currentUrl);
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.Vouchers.GetVouchers + "?PageNumber=1&PageSize=50&cmpId=" + fyrId);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            VoucherViewModel voucherViewModel = new VoucherViewModel();
            if (!string.IsNullOrEmpty(resultuerinfo) && resultuerinfo != "[]")
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<JournalEntryViewModel>>(resultuerinfo);
                voucherViewModel.JournalEntries = data;
            }

            voucherViewModel.Name = voucherName;
            return Page();
        }
    }
}
