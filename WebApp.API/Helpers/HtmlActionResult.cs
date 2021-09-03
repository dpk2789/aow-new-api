//using System.IO;
//using System.Net.Http;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web.Http;

//namespace WebApp.API.Helpers
//{
//    public class HtmlActionResult : IHttpActionResult
//    {
//        public HtmlActionResult(HttpRequestMessage request, string content)
//        {
//            Request = request;          
//            Content = LoadView(viewName);
//        }

//        public string Content { get; private set; }
//        public HttpRequestMessage Request { get; private set; }

//        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
//        {
//            return Task.FromResult(ExecuteResult());
//        }

//        public HttpResponseMessage ExecuteResult()
//        {
//            var response = new HttpResponseMessage();

//            if (!string.IsNullOrWhiteSpace(Content))
//                response.Content = new StringContent(Content, Encoding.UTF8, "text/html");

//            response.RequestMessage = Request;
//            return response;
//        }

//        private static string LoadView(string name)
//        {
//            var view = File.ReadAllText(Path.Combine(ViewDirectory, name + ".cshtml"));
//            return view;
//        }
//    }
//}
