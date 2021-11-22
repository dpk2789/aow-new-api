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
        public class AllVarientsViewModel
        {
            public Guid Id { get; set; }
            public int? SrNo { get; set; }
            public decimal ItemsTotal { get; set; }
            public string ItemName { get; set; }
            public Guid ProductId { get; set; }
            public Guid VoucherId { get; set; }
            public string PurchasePrice { get; set; }          
        }
        [BindProperty]
        public IEnumerable<AllVarientsViewModel> CurrentStock { get; set; }
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
                var data = JsonConvert.DeserializeObject<IEnumerable<AllVarientsViewModel>>(resultuerinfo);
                CurrentStock = data;
            }
            return Page();          
        }
    }
}
