using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks.FinancialYear
{
    public class SetFinancialYearModel : PageModel
    {
        private readonly ICookieHelper _cookieHelper;
        public string ApiUrl { get; }
        public SetFinancialYearModel(ICookieHelper cookieHelper)
        {
            ApiUrl = ApiUrls.Rootlocal;
            _cookieHelper = cookieHelper;
        }
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            public string Name { get; set; }
            public string CompanyId { get; set; }
            public string ReturnUrl { get; set; }
            public SelectList FinancialYearSelectList { get; set; }
        }

        public class FinancialYearSelectAddViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
        }
        public async Task<IEnumerable<FinancialYearSelectAddViewModel>> GetFinancialYears()
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return null;
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.FinancialYear.GetFinancialYears + "?PageNumber=1&PageSize=10&cmpId=" + cmpid);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);
            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            IEnumerable<FinancialYearSelectAddViewModel> FinancialYears = null;
            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<FinancialYearSelectAddViewModel>>(resultuerinfo);
                FinancialYears = data;
            }

            return FinancialYears;
        }
        public async Task<IActionResult> OnGet(string returnUrl)
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return RedirectToPage("/");
            }
            InputModel inputModel = new InputModel
            {
                CompanyId = cmpid,
                ReturnUrl = returnUrl
            };
            Input = inputModel;
            var data = await GetFinancialYears();
            inputModel.FinancialYearSelectList = new SelectList(data, "Id", "Name");
            return Page();
        }
    }
}
