using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks.JournalEntries
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
            public string FinancialYearId { get; set; }
            public string VoucherNumber { get; set; }
            public int VoucherTypeId { get; set; }
            public string VoucherName { get; set; }

            //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd-MM-yyyy")]
            public DateTime? Date { get; set; }
            public string RefId { get; set; }
            public string Note { get; set; }
            public decimal? Total { get; set; }
            public bool? Type { get; set; }
            public virtual List<JournalEntryViewModel> JournalEntries { get; set; }
        }
        public class JournalEntryViewModel
        {
            public Guid? Id { get; set; }
            public Guid? VoucherId { get; set; }
            public string VoucherName { get; set; }           
            public string VoucherNumber { get; set; }           
            public DateTime? VoucherDate { get; set; }
            public decimal? VoucherTotal { get; set; }         
            public int? SrNo { get; set; }
            public string CrDrType { get; set; }
            public Guid LedgerId { get; set; }
            public string InvoiceAccountName { get; set; }
            public string AccountName { get; set; }
            public string RootCategory { get; set; }
            public decimal? CreditAmount { get; set; }
            public decimal? DebitAmount { get; set; }
            public string RefId { get; set; }
            public decimal? ItemAmount { get; set; }          
        }

        public async Task<IActionResult> OnGet(Guid Id)
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return RedirectToPage("/");
            }
            using var client = new HttpClient();
            var updateProductsUri = new Uri(ApiUrls.Vouchers.GetVoucher + "?id=" + Id);

            var userAccessToken = User.Claims.FirstOrDefault(x => x.Type == "AcessToken").Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            var postTask = await client.GetAsync(updateProductsUri);
            var result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (result != null)
            {
                var data = JsonConvert.DeserializeObject<InputModel>(result);
                Input = data;
            }          
            return Page();
        }
    }
}
