using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks.Ledgers
{
    public class AddModel : PageModel
    {
        private readonly ICookieHelper _cookieHelper;
        public string ApiUrl { get; }
        public AddModel(ICookieHelper cookieHelper)
        {
            ApiUrl = ApiUrls.Rootlocal;
            _cookieHelper = cookieHelper;
        }
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            public string Name { get; set; }
            public string CompanyId { get; set; }
            public string CategoryId { get; set; }
            public SelectList ProductCategorySelectList { get; set; }
        }

        public class LedgerCategorySelectViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
        }
        [BindProperty]
        public IEnumerable<LedgerCategorySelectViewModel> LedgerCategories { get; set; }
        public async Task<IEnumerable<LedgerCategorySelectViewModel>> GetCategories()
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return null;
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.LedgerCategories.GetLedgerCategories + "?PageNumber=1&PageSize=10&cmpId=" + cmpid);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<LedgerCategorySelectViewModel>>(resultuerinfo);
                LedgerCategories = data;
            }

            return LedgerCategories;
        }
        public async Task<IActionResult> OnGet()
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return RedirectToPage("/");
            }
            InputModel inputModel = new InputModel();
            inputModel.CompanyId = cmpid;
            Input = inputModel;
            var data = await GetCategories();
            inputModel.ProductCategorySelectList = new SelectList(data, "Id", "Name");
            return Page();
        }
    }
}
