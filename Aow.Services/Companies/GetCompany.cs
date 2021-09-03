using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Companies
{
    [Service]
    public class GetCompany
    {
        private ICompanyRepository _companyRepository;
        private readonly UserManager<AppUser> _userManager;
        public GetCompany(ICompanyRepository companyRepository, UserManager<AppUser> userManager)
        {
            _companyRepository = companyRepository;
            _userManager = userManager;
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
