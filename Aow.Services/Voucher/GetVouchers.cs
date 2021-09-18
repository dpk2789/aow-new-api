using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.JournalEntry
{
    [Service]
    public class GetVouchers
    {
        private IRepositoryWrapper _repoWrapper;
        public GetVouchers(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetJournalEntriesResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public IEnumerable<GetJournalEntriesResponse> Do(PagingParameters pagingParameters, Guid FinancialYearId)
        {
            var financialYear = _repoWrapper.FinancialYearRepo.GetFinancialYear(FinancialYearId);
            if (financialYear == null)
            {
                return null;
            };
            var list = _repoWrapper.VoucherRepo.GetVouchers(pagingParameters, financialYear.Id).GetAwaiter().GetResult();
            var newList = list.Select(x => new GetJournalEntriesResponse
            {
                Id = x.Id,
                Name = x.VoucherName,
            });

            return newList;
        }
    }
}
