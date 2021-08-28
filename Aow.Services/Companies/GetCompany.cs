using Aow.Infrastructure.Repositories;
using System;

namespace Aow.Services.Companies
{
    [Service]
    public class GetCompany
    {
        private ICompanyRepository _companyRepository;
        public GetCompany(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
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
            var company = _companyRepository.GetCompany(id);
            GetCompanyResponse getCompanyResponse = new GetCompanyResponse
            {
                Id = company.Id,
                Name = company.Name
            };

            return getCompanyResponse;
        }
    }
}
