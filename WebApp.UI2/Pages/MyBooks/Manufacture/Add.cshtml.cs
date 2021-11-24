using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks.Manufacture
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
            public Guid Id { get; set; }
            public string FinancialYearId { get; set; }
            public decimal VoucherNumber { get; set; }
            public int VoucherTypeId { get; set; }
            public string VoucherName { get; set; }         
            public DateTime Date { get; set; }
            public string RefId { get; set; }
            public string Note { get; set; }
            public decimal Total { get; set; }
            public bool? Type { get; set; }
            public virtual List<InputVarientsViewModel> InputVarients { get; set; }
            public virtual List<OutputVarientsViewModel> OutputVarients { get; set; }
        }
        public class InputVarientsViewModel
        {
            public decimal? Quantity { get; set; }
            public Guid? StockProductVariantId { get; set; }
            public string VarientName { get; set; }
        }
        public class OutputVarientsViewModel
        {
            public decimal? Quantity { get; set; }
            public Guid? StockProductVariantId { get; set; }
            public string VarientName { get; set; }
        }
        public IActionResult OnGet()
        {
            var fyrId = _cookieHelper.Get("fYrCookee");
            if (string.IsNullOrEmpty(fyrId) && string.IsNullOrEmpty(fyrId))
            {
                return RedirectToPage("/");
            }
            InputModel inputModel = new InputModel();
            inputModel.FinancialYearId = fyrId;          
            Input = inputModel;
            return Page();
        }
    }
}
