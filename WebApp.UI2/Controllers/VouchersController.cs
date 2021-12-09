using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApp.UI2.Helpers;
using WebApp.UI2.Models;

namespace WebApp.UI2.Controllers
{
    public class VouchersController : Controller
    {
        private readonly ICookieHelper _cookieHelper;
        public VouchersController(ICookieHelper cookieHelper)
        {
            _cookieHelper = cookieHelper;
        }

        public async Task<JsonResult> GetLedgersForJournalEntry(string term, string HeadName, string crdr)
        {
            var cmpid = _cookieHelper.Get("cmpCookee");
            List<LedgerCategorySelectViewModel> LedgerCategories = null;
            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return null;
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.Ledger.GetLedgers + "?PageNumber=1&PageSize=100&cmpId=" + cmpid);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IList<LedgerCategorySelectViewModel>>(resultuerinfo);
                LedgerCategories = data.Where(c => c.Name.IndexOf(term, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
                // LedgerCategories = data.Where(ii => ii.Name.IndexOf(term)>-1).ToList();
            }
            return Json(LedgerCategories.Select(m => new
            {
                id = m.Id,
                value = m.Name,
                //rootCategory = m.Parent.Name,
                // label = String.Format("{1}", m.Name),
            }));
        }

        public async Task<JsonResult> GetItemsForInvoice(string term, string HeadName, string crdr)
        {
            var cmpid = _cookieHelper.Get("cmpCookee");
            List<ProductViewModel> ProductViewModelList = null;
            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return null;
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.Product.GetProducts + "?PageNumber=1&PageSize=100&cmpId=" + cmpid);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IList<ProductViewModel>>(resultuerinfo);
                ProductViewModelList = data.Where(c => c.Name.IndexOf(term, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
                // LedgerCategories = data.Where(ii => ii.Name.IndexOf(term)>-1).ToList();
            }
            return Json(ProductViewModelList.Select(m => new
            {
                id = m.Id,
                value = m.Name,
                label = String.Format("{0}/{1}/{2}", m.Code, m.Name, m.SalePrice),
                mRPPerUnit = m.SalePrice,
                name = m.Name,
                productCategoryId = m.ProductCategoryId,
                productId = m.ProductId,
                itemtype = m.ItemType != null ? m.ItemType.Replace(" ", string.Empty) : null,
            }));
        }

        public async Task<JsonResult> GetVarientsForInvoice(string term, string productId)
        {
            List<ProductViewModel> ProductViewModelList = null;
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.ProductVarients.GetAllVarientsByProduct + "?productId=" + productId);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IList<ProductViewModel>>(resultuerinfo);
                ProductViewModelList = data.Where(c => c.Name.IndexOf(term, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
                // LedgerCategories = data.Where(ii => ii.Name.IndexOf(term)>-1).ToList();
            }
            return Json(ProductViewModelList.Select(m => new
            {
                id = m.Id,
                value = m.Name,
                label = String.Format("{0}/{1}/{2}", m.Code, m.Name, m.SalePrice),
                mRPPerUnit = m.SalePrice,
                name = m.Name,
                productId = m.ProductId,
            }));
        }

        public async Task<JsonResult> GetVarientsForManufacturing(string term)
        {
            var cmpid = _cookieHelper.Get("cmpCookee");
            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return null;
            }
            List<ProductViewModel> ProductViewModelList = null;
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.ProductVarients.GetAllVarientsByCompany + "?companyId=" + cmpid);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IList<ProductViewModel>>(resultuerinfo);
                ProductViewModelList = data.Where(c => c.Name.IndexOf(term, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
                // LedgerCategories = data.Where(ii => ii.Name.IndexOf(term)>-1).ToList();
            }
            return Json(ProductViewModelList.Select(m => new
            {
                id = m.Id,
                value = m.Name,
                label = String.Format("{0}/{1}/{2}", m.Code, m.Name, m.SalePrice),
                mRPPerUnit = m.SalePrice,
                name = m.Name,
                productId = m.ProductId,
            }));
        }

        public async Task<JsonResult> GetStoreItemsForManufacturing(string term)
        {
            var cmpid = _cookieHelper.Get("cmpCookee");
            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return null;
            }
            List<ProductViewModel> ProductViewModelList = null;
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.Stock.GetStocks + "?companyId=" + cmpid);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IList<ProductViewModel>>(resultuerinfo);
                ProductViewModelList = data.Where(c => c.Name.IndexOf(term, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
                // LedgerCategories = data.Where(ii => ii.Name.IndexOf(term)>-1).ToList();
            }
            return Json(ProductViewModelList.Select(m => new
            {
                id = m.Id,
                value = m.Name,
                label = String.Format("{0}/{1}/{2}", m.Code, m.Name, m.SalePrice),
                quantity = m.Quantity,
                description = m.Description,
                mRPPerUnit = m.MRPPerUnit,
                name = m.Name,
                productId = m.ProductId,
            }));
        }
        public async Task<JsonResult> GetStockVarientsForManufacturing(string term)
        {
            var cmpid = _cookieHelper.Get("cmpCookee");
            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return null;
            }
            List<StockVariantViewModel> ProductViewModelList = null;
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.StockVarients.GetAllStoreVarientsByCompany + "?companyId=" + cmpid);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IList<StockVariantViewModel>>(resultuerinfo);
                ProductViewModelList = data.Where(c => c.Name.IndexOf(term, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
                // LedgerCategories = data.Where(ii => ii.Name.IndexOf(term)>-1).ToList();
            }
            return Json(ProductViewModelList.Select(m => new
            {
                id = m.Id,
                value = m.Name,
                label = String.Format("{0}/{1}-{2}", m.UniqueNumber, m.Name, m.Quantity),
                mRPPerUnit = m.SalePrice,
                name = m.Name,
                varientId = m.ProductVarientId,
            }));
        }

        public async Task<JsonResult> GetProductsForSundryItems(string term, string HeadName, string crdr)
        {
            var cmpid = _cookieHelper.Get("cmpCookee");
            List<ProductViewModel> ProductViewModelList = null;
            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return null;
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.SundryItem.GetSundryItems + "?PageNumber=1&PageSize=100&cmpId=" + cmpid);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IList<ProductViewModel>>(resultuerinfo);
                ProductViewModelList = data.Where(c => c.Name.IndexOf(term, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
                // LedgerCategories = data.Where(ii => ii.Name.IndexOf(term)>-1).ToList();
            }
            return Json(ProductViewModelList.Select(m => new
            {
                id = m.Id,
                value = m.Name,
                label = String.Format("{0}/{1}/{2}", m.Code, m.Name, m.SalePrice),
                percent = m.Percent,
                name = m.Name,
                productCategoryId = m.ProductCategoryId,
                itemtype = m.ItemType != null ? m.ItemType.Replace(" ", string.Empty) : null,
            }));
        }
    }
}
