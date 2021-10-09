using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApp.UI2.Pages
{
    public class ContactUsModel : PageModel
    {
        [BindProperty]
        public ContactUsViewModel Input { get; set; }
        public class ContactUsViewModel
        {
            public int Id { get; set; }
            public string To { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public bool IsMailSend { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();            

            MailMessage msz = new MailMessage();
            msz.From = new MailAddress(Input.Email);//Email which you are getting 
                                                    //from contact us page 

            msz.To.Add("support@accountingonweb.com");//Where mail will be sent 
            msz.Subject = Input.Subject;
            msz.Body = Input.Body;
            SmtpClient smtp = new SmtpClient();

            smtp.Host = "mail5009.site4now.net";
            smtp.Port = 8889;
            smtp.Credentials = new System.Net.NetworkCredential
                            ("support@accountingonweb.com", "Password#$5");

            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = true;
             smtp.Send(msz);
            Input.IsMailSend = true;
            ViewData["ConfirmMsg"] = "Your message has been sent. Thank you!";
            return Page();
            // If we got this far, something failed, redisplay form
        }
    }
}
