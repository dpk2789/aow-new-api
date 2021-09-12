using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface ILedgerCategoryRepository : IRepositoryBase<LedgerCategory>
    {
        Task<PagedList<LedgerCategory>> GetLedgerCategories(PagingParameters ownerParameters, Guid cmpId);
        LedgerCategory GetLedgerCategory(Guid Id);
    }
}
