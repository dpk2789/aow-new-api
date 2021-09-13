using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.Ledger
{
    [Service]
    public class GetLedgers
    {
        private IRepositoryWrapper _repoWrapper;
        public GetLedgers(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetLedgersResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public IEnumerable<GetLedgersResponse> Do(PagingParameters pagingParameters, Guid companyId)
        {
            var user = _repoWrapper.CompanyRepo.GetCompany(companyId);
            if (user == null)
            {
                return null;
            };
            var list = _repoWrapper.LedgerRepositoryRepo.GetLedgers(pagingParameters, companyId).GetAwaiter().GetResult();
            var newList = list.Select(x => new GetLedgersResponse
            {
                Id = x.Id,
                Name = x.Name,
            });

            return newList;
        }
    }
}
