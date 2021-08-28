namespace WebApp.UI2.Helpers
{
    public static class ApiUrls
    {
        public const string Rootlocal = "https://localhost:44329";
        //public const string Rootlocal = "http://api.robustpackagingeshop.com";
        public static class Identity
        {
            public const string Login = Rootlocal + "/api/user/login";
            public const string Register = Rootlocal + "/api/user/register";
            public const string ConfirmEmail = Rootlocal + "/api/user/ConfirmEmail";
        }
        public static class Product
        {
            public const string GetProducts = Rootlocal + "/api/Products/GetProducts";
            public const string GetProduct = Rootlocal + "/api/Products/GetProduct";
            public const string GetProductByName = Rootlocal + "/api/Products/GetProductByName";
            public const string Create = Rootlocal + "/api/Products/CreateProduct";
            public const string Update = Rootlocal + "/api/Products/UpdateProduct";
            public const string Delete = Rootlocal + "/Products/{postId}";
        }
        public static class Company
        {
            public const string GetCompanies = Rootlocal + "/api/Companies/GetCompanies";
            public const string GetCompany = Rootlocal + "/api/Company/GetCompany";
            public const string GetCompanyByName = Rootlocal + "/api/Companies/GetCompanyByName";
            public const string Create = Rootlocal + "/api/Company/CreateCompany";
            public const string Update = Rootlocal + "/api/Company/UpdateCompany";
            public const string Delete = Rootlocal + "/Companies/{postId}";
        }
        public static class Cart
        {
            public const string GetCartItems = Rootlocal + "/api/Cart/GetCartItems";
            public const string Get = Rootlocal + "/api/Cart/addtocart/{postId}";
            public const string Create = Rootlocal + "/api/Cart/addtocart/companies";
            public const string Update = Rootlocal + "/Cart/{postId}";
            public const string Delete = Rootlocal + "/Cart/{postId}";
        }

        public static class Order
        {
            public const string Create = Rootlocal + "/api/Order/CreateOrder";
        }

    }
}
