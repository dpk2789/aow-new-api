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
        IProductCategoryRepository ProductCategoryRepo { get; }
        IProductAttributeRepository ProductAttributeRepo { get; }
        IProductAttributeOptionRepository ProductAttributeOptionRepo { get; }
        Task<int> SaveNew();
        int Save();
    }
}
