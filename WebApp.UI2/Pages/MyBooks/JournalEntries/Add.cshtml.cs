using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks.JournalEntries
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
            public decimal VoucherNumber { get; set; }
            public int VoucherTypeId { get; set; }
            public string VoucherName { get; set; }

            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd-MM-yyyy")]
            public DateTime Date { get; set; }
            public string RefId { get; set; }
            public string Note { get; set; }
            public decimal Total { get; set; }
            public bool? Type { get; set; }
            public virtual List<JournalEntryViewModel> JournalEntryViewModel { get; set; }
        }
        public class JournalEntryViewModel
        {
            public Guid? Id { get; set; }
            public Guid? VoucherId { get; set; }
            public string VoucherName { get; set; }
            [Required]
            public decimal VoucherInvoice { get; set; }
            [Required]
            public DateTime VoucherDate { get; set; }
            public decimal? VoucherTotal { get; set; }
            public decimal? OutstandingAmount { get; set; }
            public bool Paid { get; set; }
            public int? SrNo { get; set; }
            public string CrDrType { get; set; }
            public Guid LedgerId { get; set; }
            public string InvoiceAccountName { get; set; }
            public string AccountName { get; set; }
            public string RootCategory { get; set; }
            public decimal? CreditAmount { get; set; }
            public decimal? DebitAmount { get; set; }
            public string RefId { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ItemAmount { get; set; }


            public string Note { get; set; }
            public string AccountType { get; set; }
            public decimal? TaxWithExtendedPrice { get; set; }

            public string Description { get; set; }
            public decimal? Freight { get; set; }
            public decimal? Packaging { get; set; }
            public decimal? OtherExpenses { get; set; }
        }
        public IActionResult OnGet(string voucherName)
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return RedirectToPage("/");
            }
            InputModel inputModel = new InputModel();
            Input = inputModel;
            return Page();
        }
    }
}
