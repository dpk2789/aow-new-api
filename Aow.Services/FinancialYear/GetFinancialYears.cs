using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.FinancialYear
{
    [Service]
    public class GetFinancialYears
    {
        private IRepositoryWrapper _repoWrapper;
        public GetFinancialYears(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class FinancialYearsResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public DateTime? Start { get; set; }
            public DateTime? End { get; set; }
            public bool IsActive { get; set; }
            public bool? IsLocked { get; set; }
        }

        public IEnumerable<FinancialYearsResponse> Do(PagingParameters pagingParameters, Guid companyId)
        {
            var user = _repoWrapper.CompanyRepo.GetCompany(companyId);
            if (user == null)
            {
                return null;
            };
            var list = _repoWrapper.FinancialYearRepo.GetFinancialYears(pagingParameters, companyId).GetAwaiter().GetResult();
            var newList = list.Select(x => new FinancialYearsResponse
            {
                Id = x.Id,
                Name = x.Name,
                Start = x.Start,
                End = x.End,
                IsLocked = x.IsLocked
            });

            return newList;
        }
    }
}
