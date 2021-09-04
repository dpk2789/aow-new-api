using Aow.Infrastructure.IRepositories;
using System;

namespace Aow.Services.Companies
{
    [Service]
    public class GetCompany
    {
        private IRepositoryWrapper _repoWrapper;
        public GetCompany(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        public class GetCompanyResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public GetCompanyResponse Do(Guid id)
        {
            var company = _repoWrapper.CompanyRepo.GetCompany(id);

            GetCompanyResponse getCompanyResponse = new GetCompanyResponse
            {
                Id = company.Id,
                Name = company.Name
            };

            return getCompanyResponse;
        }
    }
}
