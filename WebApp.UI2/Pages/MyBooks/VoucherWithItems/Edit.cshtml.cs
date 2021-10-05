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

namespace WebApp.UI2.Pages.MyBooks.VoucherWithItems
{
    public class EditModel : PageModel
    {
        private readonly ICookieHelper _cookieHelper;
        public string ApiUrl { get; }
        public EditModel(ICookieHelper cookieHelper)
        {
            ApiUrl = ApiUrls.Rootlocal;
            _cookieHelper = cookieHelper;
        }
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            public Guid? Id { get; set; }
            public string FinancialYearId { get; set; }
            public string VoucherName { get; set; }
            public string VoucherNumber { get; set; }
            public int? VoucherTypeId { get; set; }
            public Guid? LedgerId { get; set; }
            public string LedgerName { get; set; }

            //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd-MM-yyyy")]
            public DateTime? Date { get; set; }
            public string RefId { get; set; }
            public string Note { get; set; }
            public decimal? ItemsTotal { get; set; }
            public decimal? SundryTotal { get; set; }
            public decimal? Total { get; set; }
            public bool? Type { get; set; }
            public virtual List<VoucherItemsViewModel> VoucherItems { get; set; }
            public virtual List<VoucherSundryItemsViewModel> SundryItems { get; set; }
        }
        public class VoucherItemsViewModel
        {
            public Guid Id { get; set; }
            public int? SrNo { get; set; }
            public string ItemName { get; set; }
            public string Description { get; set; }
            public string ItemType { get; set; }
            public decimal? DiscountRatePerUnit { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ItemAmount { get; set; }         
            public decimal Price { get; set; }
            public Guid ProductId { get; set; }
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

        public async Task<IActionResult> OnGet(Guid Id)
        {
            var cmpid = _cookieHelper.Get("cmpCookee");

            if (string.IsNullOrEmpty(cmpid) && string.IsNullOrEmpty(cmpid))
            {
                return RedirectToPage("/");
            }
            using var client = new HttpClient();
            var updateProductsUri = new Uri(ApiUrls.VouchersInvoice.GetVoucherInvoice + "?id=" + Id);

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
