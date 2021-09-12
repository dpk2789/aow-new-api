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

namespace WebApp.UI2.Pages.MyBooks.Products
{
    public class UpdateModel : PageModel
    {
        private readonly ICookieHelper _cookieHelper;
        public string ApiUrl { get; }
        public UpdateModel(ICookieHelper cookieHelper)
        {
            ApiUrl = ApiUrls.Rootlocal;
            _cookieHelper = cookieHelper;
        }
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string CompanyName { get; set; }
            public string ProductCategoryId { get; set; }
            public SelectList ProductCategorySelectList { get; set; }
        }
        public class ProductCategorySelectUpdateViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
        }
        [BindProperty]
        public IEnumerable<ProductCategorySelectUpdateViewModel> ProductCategories { get; set; }
        public async Task<IEnumerable<ProductCategorySelectUpdateViewModel>> GetCategories()
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return null;
            }
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.ProductCategories.GetProductCategories + "?PageNumber=1&PageSize=10&cmpId=" + cmpid);

            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<ProductCategorySelectUpdateViewModel>>(resultuerinfo);
                ProductCategories = data;
            }
            return ProductCategories;
        }
        public async Task<IActionResult> OnGet(Guid Id)
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return RedirectToPage("/");
            }
            using var client = new HttpClient();
            var updateProductsUri = new Uri(ApiUrls.Product.GetProduct + "?id=" + Id);

            var userAccessToken = User.Claims.FirstOrDefault(x => x.Type == "AcessToken").Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var postTask = await client.GetAsync(updateProductsUri);
            var result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (result != null)
            {
                var data = JsonConvert.DeserializeObject<InputModel>(result);
                Input = data;
            }

            var categories = await GetCategories();
            Input.ProductCategorySelectList = new SelectList(categories, "Id", "Name", Input.ProductCategoryId);
            return Page();
        }
    }
}
