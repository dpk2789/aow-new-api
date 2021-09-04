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
            var product = _repoWrapper.CompanyRepo.GetCompany(request.Id);
            product.Name = request.Name;
            _repoWrapper.CompanyRepo.Update(product);
            int i = await _repoWrapper.SaveNew();
            if (i <= 0)
            {
                return new UpdateCompanyResponse
                {
                    Name = product.Name,
                    Success = false
                };
            }
            else
            {
                return new UpdateCompanyResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Success = true
                };
            }
        }
    }
}
