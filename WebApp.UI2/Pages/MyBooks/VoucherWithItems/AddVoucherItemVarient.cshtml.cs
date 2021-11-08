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
        public class AddVoucherItemVarientViewModel
        {
            public Guid VarientId { get; set; }
            public string Name { get; set; }
            public string Quantity { get; set; }
            public string PurchasePrice { get; set; }
        }
     
        [BindProperty] public AddVoucherItemVarientViewModel Input { get; set; }
        public async Task<IActionResult> OnGet(Guid id)
        {
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.ProductVarients.GetProductAttributesAndOptions + "?productId=" + id);
            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            AddVoucherItemVarientViewModel inputModel = new AddVoucherItemVarientViewModel();
            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IList<AddVoucherItemVarientViewModel>>(resultuerinfo);
                //inputModel = data;
            }
            inputModel.VarientId = id;
            Input = inputModel;
            return Page();
        }
    }
}
