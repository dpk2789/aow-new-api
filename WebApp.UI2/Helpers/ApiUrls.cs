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
      
        public static class Company
        {
            public const string GetCompanies = Rootlocal + "/api/Companies/GetCompanies";
            public const string GetCompany = Rootlocal + "/api/Company/GetCompany";
            public const string GetCompanyByName = Rootlocal + "/api/Companies/GetCompanyByName";
            public const string Create = Rootlocal + "/api/Company/CreateCompany";
            public const string Update = Rootlocal + "/api/Company/UpdateCompany";
            public const string Delete = Rootlocal + "/Companies/{postId}";
        }
        public static class FinancialYear
        {
            public const string GetFinancialYears = Rootlocal + "/api/FinancialYear/GetFinancialYears";
            public const string GetFinancialYear = Rootlocal + "/api/FinancialYear/GetFinancialYear";
            public const string GetCompanyByName = Rootlocal + "/api/FinancialYear/GetCompanyByName";
            public const string Create = Rootlocal + "/api/FinancialYear/AddFinancialYear";
            public const string Update = Rootlocal + "/api/FinancialYear/UpdateFinancialYear";
            public const string Delete = Rootlocal + "/FinancialYear/{postId}";
        }
        public static class ProductCategories
        {
            public const string GetProductCategories = Rootlocal + "/api/ProductCategories/GetProductCategories";
            public const string GetProductCategory = Rootlocal + "/api/ProductCategories/GetProductCategory";          
            public const string Create = Rootlocal + "/api/ProductCategories/AddProductCategory";
            public const string Update = Rootlocal + "/api/ProductCategories/UpdateProductCategory";
            public const string Delete = Rootlocal + "api/ProductCategories/DeleteProductCategory/{postId}";
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

        public static class LedgerCategories
        {
            public const string GetLedgerCategories = Rootlocal + "/api/LedgerCategory/GetLedgerCategories";
            public const string GetLedgerCategory = Rootlocal + "/api/LedgerCategory/GetLedgerCategory";
            public const string Create = Rootlocal + "/api/LedgerCategory/AddProductCategory";
            public const string Update = Rootlocal + "/api/LedgerCategory/UpdateProductCategory";
            public const string Delete = Rootlocal + "api/LedgerCategory/DeleteProductCategory/{postId}";
        }
        public static class Ledger
        {
            public const string GetLedgers = Rootlocal + "/api/Ledger/GetLedgers";
            public const string GetLedger = Rootlocal + "/api/Ledger/GetLedger";
            public const string GetProductByName = Rootlocal + "/api/Ledger/GetProductByName";
            public const string Create = Rootlocal + "/api/Ledger/AddLedger";
            public const string Update = Rootlocal + "/api/Ledger/UpdateLedger";
            public const string Delete = Rootlocal + "/Ledger/{postId}";
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
