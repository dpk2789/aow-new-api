using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<CompaniesResponse>> Do(PagingParameters pagingParameters, string userName)
        {
            var user = await _repoWrapper.UserRepo.GetUserByName(userName);
            if (user == null)
            {
                return null;
            };
            var list = _repoWrapper.UserCompanyRepo.GetCompaniesByUser(pagingParameters, user.Id).GetAwaiter().GetResult();
            //  var list = _companyRepository.GetCompanies(pagingParameters).GetAwaiter().GetResult();

            var newList = list.Select(x => new CompaniesResponse
            {
                Id = x.Company.Id,
                Name = x.Company.Name
            });

            return newList;
        }
    }
}
