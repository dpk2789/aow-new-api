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
        Task<int> SaveNew();
        int Save();
    }
}
