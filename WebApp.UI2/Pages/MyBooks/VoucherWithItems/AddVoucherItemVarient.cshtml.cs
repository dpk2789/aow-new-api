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
    public class AddVoucherItemVarientModel : PageModel
    {
        private readonly ICookieHelper _cookieHelper;
        public string ApiUrl { get; }
        public AddVoucherItemVarientModel(ICookieHelper cookieHelper)
        {
            ApiUrl = ApiUrls.Rootlocal;
            _cookieHelper = cookieHelper;
        }
        public class AddVoucherItemVarientViewModel
        {
            public Guid Id { get; set; }
            public string VoucherName { get; set; }
            public int? SrNo { get; set; }
            public decimal ItemsTotal { get; set; }
            public string ItemName { get; set; }        
            public Guid ProductId { get; set; }         
            public Guid VoucherId { get; set; }       
            public string PurchasePrice { get; set; }
            public virtual List<GetVoucherItemVarientResponse> Varients { get; set; }
        }

        public class GetVoucherItemVarientResponse
        {
            public Guid Id { get; set; }
            public Guid ProductId { get; set; }
            public Guid VarientId { get; set; }
            public Guid VoucherItemId { get; set; }
            public int? SrNo { get; set; }
            public decimal? DiscountRatePerUnit { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ItemAmount { get; set; }
            public string ItemName { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
        }
        [BindProperty] public AddVoucherItemVarientViewModel Input { get; set; }
        public async Task<IActionResult> OnGet(Guid id, Guid voucherId)
        {
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.VouchersInvoice.GetVoucherItem + "?id=" + id);
            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            AddVoucherItemVarientViewModel inputModel = new AddVoucherItemVarientViewModel();
            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<AddVoucherItemVarientViewModel>(resultuerinfo);
                inputModel = data;
            }         
            inputModel.VoucherId = voucherId;
            Input = inputModel;
            return Page();
        }
    }
}
