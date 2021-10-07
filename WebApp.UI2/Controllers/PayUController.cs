using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;
using System.Text;

namespace WebApp.UI2.Controllers
{
    public class PayUController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Hash(string amount, string pinfo, string fname, string email, string mobile, string udf5)
        {
            byte[] hash;
            //Add your MarchantID;  
            string key = "add your MarchantID";
            //Add your SaltID;  
            string salt = "add your SaltID";
            //posting all the parameters required for integration.  
            string txnid = Generatetxnid();

            string d = key + "|" + txnid + "|" + amount + "|" + pinfo + "|" + fname + "|" + email + "|||||" + udf5 + "||||||" + salt;
            var datab = Encoding.UTF8.GetBytes(d);
            using (SHA512 shaM = new SHA512Managed())
            {
                hash = shaM.ComputeHash(datab);
            }

            return Json(GetStringFromHash(hash));

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
    }
}
