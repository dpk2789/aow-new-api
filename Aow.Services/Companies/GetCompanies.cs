using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.Companies
{
    [Service]
    public class GetCompanies
    {
        private IRepositoryWrapper _repoWrapper;
        public GetCompanies(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class CompaniesResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }

        public IEnumerable<CompaniesResponse> Do(PagingParameters pagingParameters, string userName)
        {
            var list = _repoWrapper.UserCompanyRepo.GetCompaniesByUser(pagingParameters, userName).GetAwaiter().GetResult();
            //  var list = _companyRepository.GetCompanies(pagingParameters).GetAwaiter().GetResult();
            var newList = list.Select(x => new CompaniesResponse
            {
                Id = x.Id,
                Name = x.Company.Name
            });

            return newList;
        }
    }
}
