using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks.VoucherWithItems
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
        [BindProperty] public VoucherInvoiceViewModel Input { get; set; }
        public class VoucherInvoiceViewModel
        {
            public Guid? Id { get; set; }
            public string FinancialYearId { get; set; }
            public decimal VoucherNumber { get; set; }
            public int VoucherTypeId { get; set; }
            public string VoucherName { get; set; }
            public Guid LedgerId { get; set; }
            public string LedgerName { get; set; }

            //  [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
            public DateTime Date { get; set; }
            public string RefId { get; set; }
            public string Note { get; set; }
            public decimal Total { get; set; }
            public decimal ItemsTotal { get; set; }
            public decimal SundryTotal { get; set; }
            public bool? Type { get; set; }          
            public virtual List<VoucherSundryItemsViewModel> VoucherSundryItemsViewModels { get; set; }
            public virtual List<VoucherItemsViewModel> VoucherItemsViewModels { get; set; }
        }
        public class VoucherSundryItemsViewModel
        {
            public Guid Id { get; set; }
            public int? SrNo { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Percent { get; set; }
            public string Description { get; set; }
            public decimal? ItemAmount { get; set; }
            public Guid LedgerId { get; set; }
            public Guid ProductId { get; set; }
        }

        public class VoucherItemsViewModel
        {
            public Guid Id { get; set; }
            public int? SrNo { get; set; }
            public string Name { get; set; }
            public string ItemType { get; set; }
            public string ItemTaxCode { get; set; }
            public string Percent { get; set; }
            public string Description { get; set; }
            public decimal? ItemAmount { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal Price { get; set; }
            public Guid LedgerId { get; set; }
            public Guid ProductId { get; set; }
        }
        public IActionResult OnGet(string voucherName)
        {
            var fyrId = _cookieHelper.Get("fYrCookee");
            if (string.IsNullOrEmpty(fyrId) && string.IsNullOrEmpty(fyrId))
            {
                return RedirectToPage("/");
            }
            VoucherInvoiceViewModel inputModel = new VoucherInvoiceViewModel();
            inputModel.FinancialYearId = fyrId;
            inputModel.VoucherName = voucherName;
            Input = inputModel;
            return Page();
        }
    }
}
