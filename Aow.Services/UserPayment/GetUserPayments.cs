using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Services.UserPayment
{
    [Service]
    public class GetUserPayments
    {
        private IRepositoryWrapper _repoWrapper;
        public GetUserPayments(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class UserPaymentsResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }

        public async Task<IEnumerable<UserPaymentsResponse>> Do(PagingParameters pagingParameters, string userName)
        {
            var user = await _repoWrapper.UserRepo.GetUserByName(userName);
            if (user == null)
            {
                return null;
            };
            var list = _repoWrapper.UserCompanyRepo.GetCompaniesByUser(pagingParameters, user.Id).GetAwaiter().GetResult();
            //  var list = _companyRepository.GetCompanies(pagingParameters).GetAwaiter().GetResult();

            var newList = list.Select(x => new UserPaymentsResponse
            {
                Id = x.Company.Id,
                Name = x.Company.Name
            });

            return newList;
        }
    }
}
