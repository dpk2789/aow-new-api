using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Companies
{
    [Service]
    public class UpdateCompany
    {
        private IRepositoryWrapper _repoWrapper;
        public UpdateCompany(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class UpdateCompanyRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }

        public class UpdateCompanyResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }
        public async Task<UpdateCompanyResponse> Do(UpdateCompanyRequest request)
        {
            var company = _repoWrapper.CompanyRepo.GetCompany(request.Id);
            if (company == null)
            {
                return null;
            }
            company.Name = request.Name;
            _repoWrapper.CompanyRepo.Update(company);
            int i = await _repoWrapper.SaveNew();
            if (i <= 0)
            {
                return new UpdateCompanyResponse
                {
                    Name = company.Name,
                    Success = false
                };
            }
            else
            {
                return new UpdateCompanyResponse
                {
                    Id = company.Id,
                    Name = company.Name,
                    Success = true
                };
            }
        }
    }
}
