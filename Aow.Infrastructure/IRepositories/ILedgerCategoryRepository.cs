using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using AowCore.Domain;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface ILedgerCategoryRepository : IRepositoryBase<LedgerCategory>
    {
        Task<PagedList<LedgerCategory>> GetLedgerCategories(PagingParameters ownerParameters);
        LedgerCategory GetLedgerCategory(Guid Id);
    }
}
