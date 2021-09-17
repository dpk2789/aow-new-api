using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.JournalEntry
{
    [Service]
    public class GetJournalEntries
    {
        private IRepositoryWrapper _repoWrapper;
        public GetJournalEntries(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetJournalEntriesResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public IEnumerable<GetJournalEntriesResponse> Do(PagingParameters pagingParameters, Guid companyId)
        {
            var user = _repoWrapper.CompanyRepo.GetCompany(companyId);
            if (user == null)
            {
                return null;
            };
            var list = _repoWrapper.JournalEntryRepo.GetJournalEntries(pagingParameters, companyId).GetAwaiter().GetResult();
            var newList = list.Select(x => new GetJournalEntriesResponse
            {
                Id = x.Id,
                Name = x.VoucherName,
            });

            return newList;
        }
    }
}
