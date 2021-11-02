using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks.ProductCategoryAttributes
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
        public class ProductsAttributeViewModel
        {           
            public Guid ProductCategoryId { get; set; }
            public string Name { get; set; }
            public string ProductName { get; set; }
            public string OptionValue { get; set; }

        }
        [BindProperty] public ProductsAttributeViewModel Input { get; set; }
        public IActionResult OnGet(Guid id)
        {
            ProductsAttributeViewModel inputModel = new ProductsAttributeViewModel();
            inputModel.ProductCategoryId = id;
            Input = inputModel;
            return Page();
        }
    }
}
