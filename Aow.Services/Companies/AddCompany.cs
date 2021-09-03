using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Companies
{
    [Service]
    public class AddCompany
    {
        private IRepositoryWrapper _repoWrapper;
        private readonly UserManager<AppUser> _userManager;

        public AddCompany(IRepositoryWrapper repoWrapper, UserManager<AppUser> userManager)
        {
            _repoWrapper = repoWrapper;
            _userManager = userManager;
        }
        public class CreateRequest
        {
            public string UserName { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }

        public class CreateResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }
        public async Task<CreateResponse> Do(CreateRequest request)
        {
            Guid companyId = Guid.NewGuid();
            var user = await _userManager.FindByEmailAsync(request.UserName);
            if (user == null)
            {
                return null;
            };
            var company = new Company
            {
                Id = companyId,
                Name = request.Name,
            };
            _repoWrapper.CompanyRepo.Create(company);
            var appUserCompany = new AppUserCompany
            {
                Id = Guid.NewGuid(),
                CompanyId = companyId,
                ApplicationUserId = user.Id
            };
            _repoWrapper.UserCompanyRepo.Create(appUserCompany);

            int i = await _repoWrapper.SaveNew();

            if (i <= 0)
            {
                return new CreateResponse
                {
                    Name = company.Name,
                    Success = false
                };
            }
            else
            {
                return new CreateResponse
                {
                    Id = company.Id,
                    Name = company.Name,
                    Success = true
                };
            }

        }
    }
}
