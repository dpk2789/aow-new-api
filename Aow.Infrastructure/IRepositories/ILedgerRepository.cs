using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface ILedgerRepository : IRepositoryBase<Ledger>
    {
        Task<PagedList<Ledger>> GetLedgers(PagingParameters ownerParameters, Guid companyId);
        Ledger GetLedgerByName(Guid cmpId, string ledgerName);
        Ledger GetLedger(Guid Id);
    }
}
