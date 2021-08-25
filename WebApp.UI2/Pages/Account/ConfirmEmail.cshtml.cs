using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using WebApp.UI2.Helpers;

namespace WebApp.RazorPages.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        [TempData]
        public string StatusMessage { get; set; }
        public class ConfirmEmailResponse
        {           
            public string StatusMessage { get; set; }
            public bool Success { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            using var client = new HttpClient();
            var u = new Uri(ApiUrls.Identity.ConfirmEmail);

            var json = JsonConvert.SerializeObject(new { userId, code });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var postTask = await client.PostAsync(u, content);
            //postTask.Wait();
            var result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<ConfirmEmailResponse>(result);
            return Page();
        }
    }
}
