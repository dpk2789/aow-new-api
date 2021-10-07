using Microsoft.AspNetCore.Mvc;
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

        public ActionResult Hash(string txnid, string key, string salt, string amount, string pinfo, string fname, string email, string mobile, string udf5)
        {
            byte[] hash;


            string d = key + "|" + txnid + "|" + amount + "|" + pinfo + "|" + fname + "|" + email + "|||||" + udf5 + "||||||" + salt;
            var datab = Encoding.UTF8.GetBytes(d);
            using (SHA512 shaM = new SHA512Managed())
            {
                hash = shaM.ComputeHash(datab);
            }

            return Json(GetStringFromHash(hash));

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
