using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public interface IFinancialYearRepository : IRepositoryBase<FinancialYear>
    {
        FinancialYear GetFinancialYear(Guid Id);
        Task<PagedList<FinancialYear>> GetFinancialYears(PagingParameters ownerParameters, Guid cmpId);
    }
}
