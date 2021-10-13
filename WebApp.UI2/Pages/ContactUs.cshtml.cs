using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApp.UI2.Pages
{
    public class ContactUsModel : PageModel
    {
        private readonly IEmailSender _emailSender;
        public ContactUsModel(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
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

            // await _emailSender.SendEmailAsync(Input.Email, "test sub", "test body");
            try
            {
                using (MailMessage msz = new MailMessage())
                {
                    msz.From = new MailAddress("shadesofweb81@gmail.com", "Accounting On Web"); //sender
                    msz.Subject = "test sub";
                    msz.Body = Input.Email + Input.Body;
                    msz.IsBodyHtml = true;
                    msz.To.Add(new MailAddress("support@accountingonweb.com"));//admin email


                    //sender credentials
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential
                                    ("shadesofweb81@gmail.com", "shadesPassword&*9");

                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Send(msz);

                    await smtp.SendMailAsync(msz);
                }
                ViewData["ConfirmMsg"] = "Your message has been sent. Thank you!";
            }
            catch (Exception ex)
            {
                ViewData["ErrorMsg"] = "Innner Exception" + ex.InnerException.Message + " <br/>" + "exception" + ex.Message;
            }

            return Page();
            // If we got this far, something failed, redisplay form
        }
    }
}
