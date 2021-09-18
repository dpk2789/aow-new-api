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

        public class VoucherViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public virtual List<JournalEntryViewModel> JournalEntryViewModel { get; set; }
        }
        public class JournalEntryViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
        // public static string AppBaseUrl => $"{_httpContextAccessor.HttpContext.Request.Scheme}://{Current.Request.Host}{Current.Request.PathBase}";
        [BindProperty]
        public IEnumerable<JournalEntryViewModel> JournalEntries { get; set; }
        public async Task<IActionResult> OnGet(string voucherName)
        {
            string currentUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host.Value + _httpContextAccessor.HttpContext.Request.Path.Value;
            var fyrId = _cookieHelper.Get("fYrCookee");
            if (string.IsNullOrEmpty(fyrId) && string.IsNullOrEmpty(fyrId))
            {
                return Redirect("/MyBooks/FinancialYear/SetFinancialYear?returnUrl=" + currentUrl);
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.JournalEntries.GetJournalEntries + "?PageNumber=1&PageSize=50&cmpId=" + fyrId);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (!string.IsNullOrEmpty(resultuerinfo) && resultuerinfo != "[]")
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<JournalEntryViewModel>>(resultuerinfo);
                JournalEntries = data;
            }
            VoucherViewModel voucherViewModel = new VoucherViewModel();
            voucherViewModel.Name = voucherName;
            return Page();
        }
    }
}
