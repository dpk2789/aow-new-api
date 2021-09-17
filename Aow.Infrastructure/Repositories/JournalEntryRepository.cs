using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class JournalEntryRepository : RepositoryBase<JournalEntry>, IJournalEntryRepository
    {
        public JournalEntryRepository(AowContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public JournalEntry GetJournalEntry(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public Task<PagedList<JournalEntry>> GetJournalEntries(PagingParameters ownerParameters, Guid FinancialYearId)
        {
            return Task.FromResult(PagedList<JournalEntry>.ToPagedList(FindAll().Where(x => x.Vouchers.FinancialYearId == FinancialYearId).OrderBy(on => on.VoucherName),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }
}
