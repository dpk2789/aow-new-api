using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApp.UI2.Helpers;

namespace WebApp.UI2.Pages.MyBooks.ProductVarients
{
    public class UpdateModel : PageModel
    {
        public class UpdateVarientViewModel
        {
            public Guid Id { get; set; }
            public Guid ProductId { get; set; }
            public string Name { get; set; }
            public IList<UpdateVarientAttributesViewModel> Attributes { get; set; }

        }
        public class UpdateVarientAttributesViewModel
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; }
            public IEnumerable<UpdateVarientAttributesOptionsViewModel> Options { get; set; }
        }

        public class UpdateVarientAttributesOptionsViewModel
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; }
        }

        [BindProperty] public UpdateVarientViewModel Input { get; set; }
        public async Task<IActionResult> OnGet(Guid id)
        {
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.ProductVarients.GetProductVarient + "?id=" + id);
            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<UpdateVarientViewModel>(resultuerinfo);
                Input = data;
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id, Guid[] OptionsSelectedOnView)
        {
            if (id != Input.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                using var client = new HttpClient();
                Uri u = new Uri(ApiUrls.ProductVarients.Update);
                //postTask.Wait();

                var json = JsonConvert.SerializeObject(new { Input.Id, Input.Name, Input.ProductId, OptionsSelectedOnView });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                //HTTP POST
                var postTask = await client.PutAsync(u, content);
                string result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            return RedirectToPage("/MyBooks/Products/Update", new { id = Input.ProductId });
        }
    }
}
