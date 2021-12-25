namespace WinFormApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var client = new RestClient("http://localhost:3000/Api/GetCustomerDetailsByCustomerId/" + id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-Token-Key", "dsds-sdsdsds-swrwerfd-dfdfd");
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            dynamic json = JsonConvert.DeserializeObject(content);
            JObject customerObjJson = json.CustomerObj;
            var customerObj = customerObjJson.ToObject<Customer>();
            return customerObj;
        }
    }
}