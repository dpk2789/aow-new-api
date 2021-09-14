using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IJournalEntryRepository : IRepositoryBase<JournalEntry>
    {
        Task<PagedList<JournalEntry>> GetJournalEntries(PagingParameters ownerParameters, Guid companyId);
        JournalEntry GetJournalEntry(Guid Id);
    }

}
