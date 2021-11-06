namespace WebApp.UI2.Helpers
{
    public static class ApiUrls
    {
         public const string Rootlocal = "https://localhost:44329";
       // public const string Rootlocal = "http://aownewapi.accountingonweb.com";
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
        public static class ProductAttributes
        {
            public const string GetProductAttributes = Rootlocal + "/api/ProductAttributes/GetProductAttributes";
            public const string GetProductAttribute = Rootlocal + "/api/ProductAttributes/GetProductAttribute";
            public const string Create = Rootlocal + "/api/ProductAttributes/AddProductAttribute";
            public const string Update = Rootlocal + "/api/ProductAttributes/UpdateProductAttribute";
            public const string Delete = Rootlocal + "api/ProductAttributes/DeleteProductCategory/{postId}";
        }
        public static class ProductAttributeOption
        {
            public const string GetProductAttributeOptions = Rootlocal + "/api/ProductAttributes/GetProductAttributes";
            public const string GetProductAttributeOption = Rootlocal + "/api/ProductAttributeOption/GetProductAttributeOption";
            public const string Create = Rootlocal + "/api/ProductAttributes/AddProductAttribute";
            public const string Update = Rootlocal + "/api/ProductAttributes/UpdateProductAttribute";
            public const string Delete = Rootlocal + "api/ProductAttributes/DeleteProductCategory/{postId}";
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
        public static class ProductVarients
        {
            public const string GetProductAttributesAndOptions = Rootlocal + "/api/ProductVarients/GetProductAttributeOptionsByProduct";
            public const string GetProductVarients = Rootlocal + "/api/ProductVarients/GetProductVarients";
            public const string GetProductVarient = Rootlocal + "/api/ProductVarients/GetProductVarient";
            public const string Create = Rootlocal + "/api/ProductVarients/AddProductVarient";
            public const string Update = Rootlocal + "/api/ProductAttributes/UpdateProductAttribute";
            public const string Delete = Rootlocal + "api/ProductAttributes/DeleteProductCategory/{postId}";
        }
    
        public static class SundryItem
        {
            public const string GetSundryItems = Rootlocal + "/api/SundryItem/GetSundryItems";
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
        public static class JournalEntries
        {
            public const string GetJournalEntries = Rootlocal + "/api/JournalEntries/GetJournalEntries";
            public const string GetJournalEntry = Rootlocal + "/api/JournalEntries/GetJournalEntry";
            public const string GetProductByName = Rootlocal + "/api/JournalEntries/GetProductByName";
            public const string Create = Rootlocal + "/api/JournalEntries/AddLedger";
            public const string Update = Rootlocal + "/api/JournalEntries/UpdateLedger";
            public const string Delete = Rootlocal + "/JournalEntries/{postId}";
        }
        public static class Vouchers
        {
            public const string GetVouchers = Rootlocal + "/api/Vouchers/GetVouchers";
            public const string GetVoucher = Rootlocal + "/api/Vouchers/GetVoucher";
            public const string GetProductByName = Rootlocal + "/api/Ledger/GetProductByName";
            public const string Create = Rootlocal + "/api/Ledger/AddLedger";
            public const string Update = Rootlocal + "/api/Ledger/UpdateLedger";
            public const string Delete = Rootlocal + "/Ledger/{postId}";
        }
        public static class VouchersInvoice
        {
            public const string GetVoucherInvoices = Rootlocal + "/api/VoucherInvoice/GetVoucherInvoice";
            public const string GetVoucherInvoice = Rootlocal + "/api/VoucherInvoice/GetVoucherInvoice";
            public const string Create = Rootlocal + "/api/VoucherInvoice/AddVoucherInvoice";
            public const string Update = Rootlocal + "/api/VoucherInvoice/UpdateLedger";
            public const string Delete = Rootlocal + "/VoucherInvoice/{postId}";
        }
        public static class Cart
        {
            public const string GetCartItems = Rootlocal + "/api/Cart/GetCartItems";
            public const string Get = Rootlocal + "/api/Cart/addtocart/{postId}";
            public const string Create = Rootlocal + "/api/Cart/addtocart/companies";
            public const string Update = Rootlocal + "/Cart/{postId}";
            public const string Delete = Rootlocal + "/Cart/{postId}";
        }

        public static class Payment
        {
            public const string GetUserPayments = Rootlocal + "/api/Payment/GetUserPayments";
            public const string Create = Rootlocal + "/api/Payment/AddUserPayment";
        }

    }
}
