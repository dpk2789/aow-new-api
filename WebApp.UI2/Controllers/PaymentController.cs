using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApp.UI2.Helpers;
using WebApp.UI2.Models.Checkout;

namespace WebApp.UI2.Controllers
{
    public class PaymentController : Controller
    {
        private readonly RazorpayClient _razorpayClient;
        private readonly IEmailSender _emailSender;
        private string RazorPayKey { get; }

        public PaymentController(IEmailSender emailSender)
        {
            RazorPayKey = "rzp_live_ovIIdD5BdthHdu";
            _razorpayClient = new RazorpayClient(RazorPayKey, "HefGk8cKCQ0ztGQbbcvBuy8d");
            _emailSender = emailSender;
        }

        public class ConfirmPaymentPayload
        {
            public string RazorpayPaymentId { get; set; }
            public string RazorpayOrderId { get; set; }
            public string RazorpaySignature { get; set; }
            public decimal amount { get; set; }
        }
        public class RazorpayResponse
        {
            public string id { get; set; }
            public string entity { get; set; }
            public decimal amount { get; set; }
            public decimal amount_paid { get; set; }
            public string currency { get; set; }
            public string receipt { get; set; }
            public string status { get; set; }
            public string offer_id { get; set; }
            public string attempts { get; set; }
            public string[] notes { get; set; }
            public string created_at { get; set; }
        }

        public ActionResult CompanyRecharge(Guid? cmpId, string amt)
        {
            decimal val = Convert.ToDecimal(amt);
            val = val * 100;
            var options = new Dictionary<string, object>
        {
            { "amount", val },
            { "currency", "INR" },
            { "receipt", "recipt_1" },
            // auto capture payments rather than manual capture
            // razor pay recommended option
            { "payment_capture", true }
        };

            var order = _razorpayClient.Order.Create(options);
            var orderId = order["id"].ToString();
            var orderJson = order.Attributes.ToString();
            return Ok(orderJson);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPayment(ConfirmPaymentPayload confirmPayment)
        {
            var attributes = new Dictionary<string, string>
        {
            { "razorpay_payment_id", confirmPayment.RazorpayPaymentId },
            { "razorpay_order_id", confirmPayment.RazorpayOrderId },
            { "razorpay_signature", confirmPayment.RazorpaySignature }
        };
            try
            {
                Utils.verifyPaymentSignature(attributes);
                // OR
                bool isValid = Utils.ValidatePaymentSignature(attributes);
                if (isValid)
                {
                    var order = _razorpayClient.Order.Fetch(confirmPayment.RazorpayOrderId);
                    dynamic orderJson = order.Attributes.ToString();
                    var data = JsonConvert.DeserializeObject<RazorpayResponse>(orderJson);
                    var payment = _razorpayClient.Payment.Fetch(confirmPayment.RazorpayPaymentId);
                    var id = payment["id"];
                    var rrn = payment["acquirer_data"];
                    var rrnNo = rrn["rrn"];
                    var email = payment["email"];
                    var contactNo =payment["contact"];
                    var numvber = payment["vpa"];
                    var test = payment.All();
                    // {[vpa, { 9950772781@upi}]}
                   // var number= test.Find(x => x.Attributes == "vpa");
                    if (payment["status"] == "captured")
                    {
                        if (User.Identity.IsAuthenticated)
                        {
                            using var client = new HttpClient();
                            var createOrderUri = new Uri(ApiUrls.Order.Create);
                            var userAccessToken = User.Claims.FirstOrDefault(x => x.Type == "AcessToken")?.Value;
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

                            var orderRequest = new OrderViewModel();
                            orderRequest.UserId = User.Identity.Name;
                            orderRequest.RazorpayOrderId = confirmPayment.RazorpayOrderId;
                            orderRequest.RazorpayPaymentId = confirmPayment.RazorpayPaymentId;
                            orderRequest.RazorpaySignature = confirmPayment.RazorpaySignature;
                            //orderRequest.a = confirmPayment.amount;

                            var request = JsonConvert.SerializeObject(orderRequest);
                            var content = new StringContent(request, Encoding.UTF8, "application/json");
                            var postOrderResult = await client.PostAsync(createOrderUri, content);
                            string orderResult = postOrderResult.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        }

                        var link = $"<a href='#'>Click here</a>";

                        // await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        //$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(url)}'>clicking here</a>.");
                        //var email = User.Identity.Name;
                        await _emailSender.SendEmailAsync(User.Identity.Name, "New Order Recieved",
                            "New Order Recieved");

                        await _emailSender.SendEmailAsync("tnd.itsolutions@gmail.com", "New Order Recieved",
                           "New Order Recieved");

                        return Ok("Payment Successful");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
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

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2").ToLower());
            }
            return result.ToString();
        }

        public ActionResult CalRechargeAmt(Guid companyId, double planAmount)
        {
            DateTime startDateTime = DateTime.Now.ToUniversalTime();
            DateTime endDateTime;
            if (planAmount == 200)
            {
                endDateTime = DateTime.Now.AddDays(30);
            }
            else
            {
                endDateTime = DateTime.Now.ToUniversalTime();
            }
            var result = new { rechargeAmount = planAmount, id = companyId, startDateTime = startDateTime.ToLongDateString(), endDateTime = endDateTime.ToLongDateString() };
            return Json(result);
        }

        public string RandomString(int size = 10, bool lowerCase = true)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

    }
}
