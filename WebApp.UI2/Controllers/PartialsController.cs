using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Controllers
{
    public class PartialsController : Controller
    {
        private readonly ICookieHelper _cookieHelper;
        public PartialsController(ICookieHelper cookieHelper)
        {
            _cookieHelper = cookieHelper;
        }
        [HttpGet]
        public async Task<IActionResult> GetFinancialYearsPartialView()
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return RedirectToPage("/");
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.FinancialYear.GetFinancialYears + "?PageNumber=1&PageSize=10&cmpId=" + cmpid);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<IEnumerable<WebApp.UI2.Pages.MyBooks.FinancialYear.IndexModel.FinancialYearViewModel>>(resultuerinfo);
            return PartialView("_FyrListPartial", data);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductCategoriesPartialView()
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return RedirectToPage("/");
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.ProductCategories.GetProductCategories + "?PageNumber=1&PageSize=10&cmpId=" + cmpid);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<IEnumerable<WebApp.UI2.Pages.MyBooks.ProductCategories.IndexModel.ProductCategoriesViewModel>>(resultuerinfo);
            string modelString = string.Empty;
            modelString = await this.RenderViewAsync("_ProductCategoryPartial", data, true);
            return Json(new { success = true, modelString });
            //  return PartialView("_ProductCategoryPartial", data);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsPartialView()
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return RedirectToPage("/");
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.Product.GetProducts + "?PageNumber=1&PageSize=10&cmpId=" + cmpid);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<IEnumerable<WebApp.UI2.Pages.MyBooks.Products.IndexModel.ProductViewModel>>(resultuerinfo);
            //string modelString = string.Empty;
            //modelString = await this.RenderViewAsync("_ProductCategoryPartial", data, true);
            //return Json(new { success = true, modelString });
            return PartialView("_ProductsListPartial", data);
        }

        [HttpGet]
        public async Task<IActionResult> GetLedgerCategoriesPartialView()
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return RedirectToPage("/");
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.LedgerCategories.GetLedgerCategories + "?PageNumber=1&PageSize=10&cmpId=" + cmpid);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<IEnumerable<WebApp.UI2.Pages.MyBooks.LedgerCategories.IndexModel.LedgerCategoriesViewModel>>(resultuerinfo);
            //string modelString = string.Empty;
            //modelString = await this.RenderViewAsync("_ProductCategoryPartial", data, true);
            //return Json(new { success = true, modelString });
            return PartialView("_LedgerCategoryListPartial", data);
        }

        [HttpGet]
        public async Task<IActionResult> GetLedgerPartialView()
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return RedirectToPage("/");
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.Ledger.GetLedgers + "?PageNumber=1&PageSize=10&cmpId=" + cmpid);
            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<IEnumerable<WebApp.UI2.Pages.MyBooks.Ledgers.IndexModel.LedgerViewModel>>(resultuerinfo);          
            return PartialView("_LedgerListPartial", data);
        }

        [HttpGet]
        public async Task<IActionResult> GetJournalEntriesPartialView()
        {
            var fyrId = _cookieHelper.Get("fYrCookee");
            var cmpid = _cookieHelper.Get("cmpCookee");            
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.Vouchers.GetVouchers + "?PageNumber=1&PageSize=50&cmpId=" + cmpid + "&fyrId=" + fyrId);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<IEnumerable<WebApp.UI2.Pages.MyBooks.JournalEntries.IndexModel.VoucherViewModel>>(resultuerinfo);
            return PartialView("_JournalEntriesListPartial", data);
        }
    }
}
