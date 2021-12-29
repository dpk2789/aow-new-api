using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Services.Companies
{
    [Service]
    public class GetAllCompanies
    {
        private IRepositoryWrapper _repoWrapper;
        public GetAllCompanies(IRepositoryWrapper repoWrapper)
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

        public async Task<IEnumerable<CompaniesResponse>> Do()
        {

            var list = await _repoWrapper.CompanyRepo.GetAllCompanies();
            //  var list = _companyRepository.GetCompanies(pagingParameters).GetAwaiter().GetResult();

            var newList = list.Select(x => new CompaniesResponse
            {
                Id = x.Id,
                Name = x.Name
            });

            return newList;
        }
    }
}
