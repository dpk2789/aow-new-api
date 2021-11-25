using Aow.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IRepositoryWrapper
    {
        ICompanyRepository CompanyRepo { get; }
        IFinancialYearRepository FinancialYearRepo { get; }
        IUserCompanyRepository UserCompanyRepo { get; }
        IUserRepository UserRepo { get; }
        IUserPaymentRepository UserPaymentRepo { get; }
        IProductCategoryRepository ProductCategoryRepo { get; }
        IProductRepository ProductRepo { get; }
        IProductAttributeRepository ProductAttributeRepo { get; }
        IProductAttributeOptionRepository ProductAttributeOptionRepo { get; }
        ILedgerCategoryRepository LedgerCategoryRepositoryRepo { get; }
        ILedgerRepository LedgerRepositoryRepo { get; }
        IVoucherRepository VoucherRepo { get; }
        IVoucherItemRepository VoucherItemRepo { get; }
        IJournalEntryRepository JournalEntryRepo { get; }
        IVoucherSundryItemRepository VoucherSundryItemRepo { get; }
        IProductVarientRepository ProductVarientRepo { get; }
        IProductVariantAndOptionRepository ProductVariantAndOptionRepo { get; }
        IVoucherItemVarientRepository VoucherItemVarientRepo { get; }
        IStockRepository StockRepo { get; }
        IStockVarientRepository StockVarientRepo { get; }
        IInputRepository InputRepo { get; }
        IOutputRepository OutputRepo { get; }
        IManufactureRepository ManufactureRepo { get; }
        Task<int> SaveNew();
        int Save();
    }
}
