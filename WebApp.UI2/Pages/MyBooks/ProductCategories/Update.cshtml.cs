using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks.ProductCategories
{
    public class UpdateModel : PageModel
    {
        public string ApiUrl { get; }
        private readonly ICookieHelper _cookieHelper;
        public UpdateModel(ICookieHelper cookieHelper)
        {
            ApiUrl = ApiUrls.Rootlocal;
            _cookieHelper = cookieHelper;
        }
        [BindProperty] public ProductCategoryViewModel Input { get; set; }
        public class ProductCategoryViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string CompanyName { get; set; }
            public string CompanyId { get; set; }
            private Guid? _parentCategoryId;

            [Display(Name = "Parent Category")]
            public Guid? ParentCategoryId
            {
                get { return _parentCategoryId; }
                set
                {
                    if (Id == value)
                        throw new InvalidOperationException("A category cannot have itself as its parent.");

                    _parentCategoryId = value;
                }
            }
        }
        public async Task<List<ProductCategoryViewModel>> GetCategories()
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid))
            {
                return null;
            }
            var categories = new List<ProductCategoryViewModel>();
            using var client = new HttpClient();
            var categoryUri = new Uri(ApiUrls.ProductCategories.GetProductCategories + "?PageNumber=1&PageSize=10&cmpId=" + cmpid);

            var userAccessToken = User.Claims.FirstOrDefault(x => x.Type == "AcessToken").Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var postTask = await client.GetAsync(categoryUri);
            var result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (result != null)
            {
                var data = JsonConvert.DeserializeObject<List<ProductCategoryViewModel>>(result);
                categories = data;
            }
            return categories;
        }
        private SelectList PopulateParentCategorySelectList(Guid? id)
        {
            SelectList selectList;
            var result = GetCategories().GetAwaiter().GetResult();
            if (id == null)
                selectList = new SelectList(result.Where(c => c.ParentCategoryId == null), "Id", "Name");
            else if (result.Count(c => c.ParentCategoryId == id) == 0)
                selectList = new SelectList(result.Where(c => c.ParentCategoryId == null && c.Id != id), "Id", "Name", id);
            else selectList = new SelectList(result, "Id", "Name");
            return selectList;
        }
        public async Task<IActionResult> OnGet(Guid? Id)
        {
            using var client = new HttpClient();
            var updateProductsUri = new Uri(ApiUrls.ProductCategories.GetProductCategory + "?id=" + Id);

            var userAccessToken = User.Claims.FirstOrDefault(x => x.Type == "AcessToken").Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var postTask = await client.GetAsync(updateProductsUri);
            var result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (result != null)
            {
                var data = JsonConvert.DeserializeObject<ProductCategoryViewModel>(result);
                Input = data;
            }
            ViewData["ParentCategoryIdSelectList"] = new SelectList(await GetCategories(), "Id", "Name", Input.ParentCategoryId);          
            return Page();

        }
    }
}
