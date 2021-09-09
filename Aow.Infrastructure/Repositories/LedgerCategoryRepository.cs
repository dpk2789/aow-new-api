using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using AowCore.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class LedgerCategoryRepository : RepositoryBase<LedgerCategory>, ILedgerCategoryRepository
    {
        public LedgerCategoryRepository(AowContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public LedgerCategory GetLedgerCategory(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public Task<PagedList<LedgerCategory>> GetLedgerCategories(PagingParameters ownerParameters)
        {
            return Task.FromResult(PagedList<LedgerCategory>.ToPagedList(FindAll().OrderBy(on => on.Name),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
        
    }
}
