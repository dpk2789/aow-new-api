using Aow.Infrastructure.IRepositories;
using System;

namespace Aow.Services.JournalEntry
{
    [Service]
    public class GetJournalEntry
    {
        private IRepositoryWrapper _repoWrapper;
        public GetJournalEntry(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetJournalEntryResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid LedgerCategoryId { get; set; }
        }
        public GetJournalEntryResponse Do(Guid id)
        {
            var ledger = _repoWrapper.LedgerRepositoryRepo.GetLedger(id);
            GetJournalEntryResponse getCompanyResponse = new GetJournalEntryResponse
            {
                Id = ledger.Id,
                LedgerCategoryId = ledger.LedgerCategoryId,
                Name = ledger.Name,
            };

            return getCompanyResponse;
        }
    }
}
