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
            public string VoucherNumber { get; set; }
            public DateTime? Date { get; set; }
            public string VoucherName { get; set; }         
            public string RefId { get; set; }
            public string Note { get; set; }        
            public bool? Type { get; set; }
            public virtual IEnumerable<JournalEntryViewModel> JournalEntries { get; set; }
        }
        public class JournalEntryViewModel
        {
            public Guid Id { get; set; }
            public Guid? VoucherId { get; set; }
            public string AccountName { get; set; }
            public string VoucherName { get; set; }
            public string VoucherInvoice { get; set; }          
            public decimal? VoucherTotal { get; set; }
            public int? SrNo { get; set; }
            public bool? OnRecord { get; set; }
            public Guid? RefAccountId { get; set; }
            public string AccountType { get; set; }
            public string CrDrType { get; set; }
            public decimal? CreditAmount { get; set; }
            public decimal? DebitAmount { get; set; }
        }

        public async Task<IActionResult> OnGet(string voucherName)
        {
            string currentUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host.Value + _httpContextAccessor.HttpContext.Request.Path.Value;
            var fyrId = _cookieHelper.Get("fYrCookee");
            var cmpid = _cookieHelper.Get("cmpCookee");
            if (string.IsNullOrEmpty(fyrId) && string.IsNullOrEmpty(fyrId))
            {
                return Redirect("/MyBooks/FinancialYear/SetFinancialYear?returnUrl=" + currentUrl);
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.Vouchers.GetVouchers + "?PageNumber=1&PageSize=50&voucherName=" + voucherName + "&fyrId=" + fyrId);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (!string.IsNullOrEmpty(resultuerinfo) && resultuerinfo != "[]")
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<VoucherViewModel>>(resultuerinfo);
                VoucherViewModelList = data;
            }
            return Page();
        }
    }
}
