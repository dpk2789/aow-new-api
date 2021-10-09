using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApp.UI2.Helpers
{
    public class EmailSender : IEmailSender
    {
        private string host;
        private int port;
        private bool enableSSL;
        private string userName;
        private string password;

        // Get our parameterized configuration
        public EmailSender(string host, int port, bool enableSSL, string userName, string password)
        {
            this.host = host;
            this.port = port;
            this.enableSSL = enableSSL;
            this.userName = userName;
            this.password = password;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = enableSSL              
            };
            return client.SendMailAsync(
                new MailMessage(userName, email, subject, htmlMessage) { IsBodyHtml = true }
            );
        }

        //public async Task SendEmailAsync(string customerEmail, string subject, string htmlMessage)
        //{
        //    using (MailMessage msz = new MailMessage())
        //    {
        //        msz.From = new MailAddress("shadesofweb81@gmail.com","Accounting On Web"); //sender
        //        msz.Subject = subject;
        //        msz.Body = customerEmail + htmlMessage;
        //        msz.IsBodyHtml = true;
        //        msz.To.Add(new MailAddress("support@accountingonweb.com"));//admin email


        //        //sender credentials
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.Port = 587;
        //        smtp.Credentials = new System.Net.NetworkCredential
        //                        ("shadesofweb81@gmail.com", "shadesPassword&*9");

        //        smtp.EnableSsl = true;
        //       // smtp.UseDefaultCredentials = true;
        //        smtp.Send(msz);

        //        await smtp.SendMailAsync(msz);
        //    }
        //}
    }
}
