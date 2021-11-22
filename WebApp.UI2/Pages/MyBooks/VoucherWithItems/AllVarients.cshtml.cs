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
    public class AllVarientsModel : PageModel
    {
        public string ApiUrl { get; }
        private readonly ICookieHelper _cookieHelper;
        public AllVarientsModel(ICookieHelper cookieHelper)
        {
            ApiUrl = ApiUrls.Rootlocal;
            _cookieHelper = cookieHelper;
        }
        public class VoucherViewModel
        {
            public Guid? Id { get; set; }         
            public string VoucherName { get; set; }
            public string VoucherNumber { get; set; }
            public DateTime? Date { get; set; }
            public decimal? ItemsTotal { get; set; }
            public virtual List<AllVarientsViewModel> VoucherItemVarients { get; set; }        
        }
        public class AllVarientsViewModel
        {
            public Guid Id { get; set; }
            public Guid? ItemId { get; set; }
            public int? SrNo { get; set; }
            public decimal? DiscountRatePerUnit { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Rate { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ItemAmount { get; set; }
            public string ItemName { get; set; }
            public string VarientName { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public Guid ProductId { get; set; }
        }
        [BindProperty]
        public VoucherViewModel Input { get; set; }
        public async Task<IActionResult> OnGet(Guid Id)
        {
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.VouchersInvoice.GetAllVoucherVarients + "?id=" + Id);
            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);
          
            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<VoucherViewModel>(resultuerinfo);
                Input = data;
            }
            return Page();          
        }
    }
}
