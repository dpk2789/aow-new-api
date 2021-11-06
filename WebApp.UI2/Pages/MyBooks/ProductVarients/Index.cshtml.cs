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

namespace WebApp.UI2.Pages.MyBooks.ProductVarients
{
    public class IndexModel : PageModel
    {
        public string ApiUrl { get; }
        private readonly ICookieHelper _cookieHelper;
        public IndexModel(ICookieHelper cookieHelper)
        {
            ApiUrl = ApiUrls.Rootlocal;
            _cookieHelper = cookieHelper;
        }

        public class ProductVarientsViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string ParentCategoryName { get; set; }
        }

        [BindProperty]
        public IEnumerable<ProductVarientsViewModel> ProductCategories { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return RedirectToPage("/");
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.va.GetProductCategories + "?PageNumber=1&PageSize=10&cmpId=" + cmpid);
            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<ProductVarientsViewModel>>(resultuerinfo);
                ProductCategories = data;
            }
            return Page();
        }
    }
}
