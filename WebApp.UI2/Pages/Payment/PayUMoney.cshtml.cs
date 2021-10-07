using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.UI2.Pages.Payment
{
    public class PayUMoneyModel : PageModel
    {
        private static HttpContext _httpContextAccessor;

        public PayUMoneyModel(HttpContext httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty] public PayUModel Input { get; set; }
        public class PayUModel
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string Amount { get; set; }
            [Required]
            public string Phone { get; set; }
            [Required]
            public string ProductInfo { get; set; }
            public string Email { get; set; }
        }
        public void OnGet()
        {
        }

        public Task<IActionResult> OnPost(FormCollection form)
        {
            try
            {
                string firstName = form["txtfirstname"].ToString();
                string amount = form["txtamount"].ToString();
                string productInfo = form["txtprodinfo"].ToString();
                string email = form["txtemail"].ToString();
                string phone = form["txtphone"].ToString();
                string surl = "http://localhost:55447/Return/Return";  //Change the success url here depending upon the port number of your local system.
                string furl = "http://localhost:55447/Return/Return";  //Change the failure url here depending upon the port number of your local system.




                RemotePost myremotepost = new RemotePost();
                //Add your MarchantID;  
                string key = "add your MarchantID";
                //Add your SaltID;  
                string salt = "add your SaltID";

                //posting all the parameters required for integration.  

                myremotepost.Url = "https://sandboxsecure.payu.in/_payment";
                myremotepost.Add("key", "4ptctt6n");
                string txnid = Generatetxnid();
                myremotepost.Add("txnid", txnid);
                myremotepost.Add("amount", amount);
                myremotepost.Add("productinfo", productInfo);
                myremotepost.Add("firstname", firstName);
                myremotepost.Add("phone", phone);
                myremotepost.Add("email", email);
                myremotepost.Add("surl", "http://localhost:55447/Return/Return");//Change the success url here depending upon the port number of your local system.  
                myremotepost.Add("furl", "http://localhost:55447/Return/Return");//Change the failure url here depending upon the port number of your local system.  
                myremotepost.Add("service_provider", "payu_paisa");
                string hashString = key + "|" + txnid + "|" + amount + "|" + productInfo + "|" + firstName + "|" + email + "|||||||||||" + salt;
                string hash = Generatehash512(hashString);
                myremotepost.Add("hash", hash);

                myremotepost.Post();
            }
            catch (Exception exp)
            {
                throw;
            }
        }

        private string Generatetxnid()
        {
            Random rnd = new Random();
            string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
            string txnid1 = strHash.ToString().Substring(0, 20);

            return txnid1;
        }

        private string Generatehash512(string text)
        {
            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512 hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }
        public class RemotePost
        {
            private System.Collections.Specialized.NameValueCollection Inputs = new System.Collections.Specialized.NameValueCollection();


            public string Url = "";
            public string Method = "post";
            public string FormName = "form1";
            private readonly object _httpContextAccessor;

            public void Add(string name, string value)
            {
                Inputs.Add(name, value);
            }

            public async void Post(HttpContext httpContext)
            {
                //await httpContext.Response.WriteAsync("<html><head>");

                //httpContext.Response.Response.Write("<html><head>");

                //System.Web.HttpContext.Current.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
                //System.Web.HttpContext.Current.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
                //for (int i = 0; i < Inputs.Keys.Count; i++)
                //{
                //    System.Web.HttpContext.Current.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
                //}
                //System.Web.HttpContext.Current.Response.Write("</form>");
                //System.Web.HttpContext.Current.Response.Write("</body></html>");

                //System.Web.HttpContext.Current.Response.End();
            }
        }
    }
}
