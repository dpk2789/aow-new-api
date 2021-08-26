using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.Companies
{
    [Service]
    public class GetCompanies
    {
        private ICompanyRepository _companyRepository;
        public GetCompanies(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public class CompaniesResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }            
        }
      
        public IEnumerable<CompaniesResponse> Do(PagingParameters pagingParameters)
        {
            var list = _companyRepository.GetCompanies(pagingParameters).GetAwaiter().GetResult();
            var newList = list.Select(x => new CompaniesResponse
            {
                Id = x.Id
            });

            return newList;
        }
    }
}
