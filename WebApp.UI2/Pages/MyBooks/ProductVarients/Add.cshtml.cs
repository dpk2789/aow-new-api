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
    public class AddModel : PageModel
    {
        public class AddVarientViewModel
        {
            public Guid ProductId { get; set; }
            public string Name { get; set; }
            public string ProductName { get; set; }
            public IList<AddVarientAttributesViewModel> AttributesViewModels { get; set; }
        }
        public class AddVarientAttributesViewModel
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public string ProductName { get; set; }
            public bool IsChecked { get; set; }
            public IEnumerable<AddVarientAttributesOptionsViewModel> AttributesOptionsViewModels { get; set; }
        }

        public class AddVarientAttributesOptionsViewModel
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; }
        }
        [BindProperty] public AddVarientViewModel Input { get; set; }
        public async Task<IActionResult> OnGet(Guid id)
        {
            using var client = new HttpClient();
            var getProductsUri = new Uri(ApiUrls.ProductVarients.GetProductAttributesAndOptions + "?productId=" + id);
            var userAccessToken = User.Claims.Where(x => x.Type == "AcessToken").FirstOrDefault().Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var getUserInfo = await client.GetAsync(getProductsUri);

            string resultuerinfo = getUserInfo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            AddVarientViewModel inputModel = new AddVarientViewModel();
            if (resultuerinfo != null)
            {
                var data = JsonConvert.DeserializeObject<IList<AddVarientAttributesViewModel>>(resultuerinfo);
                inputModel.AttributesViewModels = data;
                if (data.Count > 0)
                {
                    var attribute = data.FirstOrDefault();
                    inputModel.ProductName = attribute.ProductName;
                }
            }
            inputModel.ProductId = id;
            Input = inputModel;
            return Page();
        }

        public async Task<IActionResult> OnPost(AddVarientViewModel request, Guid[] OptionsSelectedOnView)
        {
            if (ModelState.IsValid)
            {
                request.Name = Input.Name;
                request.ProductId = Input.ProductId;
                using var client = new HttpClient();
                Uri u = new Uri(ApiUrls.ProductVarients.Create);
                //postTask.Wait();               


                var json = JsonConvert.SerializeObject(new { request.Name, request.ProductId, OptionsSelectedOnView });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                //HTTP POST
                var postTask = await client.PostAsync(u, content);
                string result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            return RedirectToPage("/MyBooks/Products/Update", new { id = request.ProductId });
        }
    }
}

