using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.FinancialYear
{
    [Service]
    public class GetFinancialYears
    {
        private IFinancialYearRepository _financialYearRepository;
        public GetFinancialYears(IFinancialYearRepository financialYearRepository)
        {
            _financialYearRepository = financialYearRepository;
        }
        public class FinancialYearsResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }

        public IEnumerable<FinancialYearsResponse> Do(PagingParameters pagingParameters)
        {
            var list = _financialYearRepository.GetFinancialYears(pagingParameters).GetAwaiter().GetResult();
            var newList = list.Select(x => new FinancialYearsResponse
            {
                Id = x.Id,
                Name = x.Name
            });

            return newList;
        }
    }
}
