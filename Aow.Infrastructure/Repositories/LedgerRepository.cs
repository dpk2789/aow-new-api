using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using AowCore.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class LedgerRepository : RepositoryBase<Ledger>, ILedgerRepository
    {
        public LedgerRepository(AowContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public Ledger GetLedger(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public Task<PagedList<Ledger>> GetLedgers(PagingParameters ownerParameters)
        {
            return Task.FromResult(PagedList<Ledger>.ToPagedList(FindAll().OrderBy(on => on.Name),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }
}
