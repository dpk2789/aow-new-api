using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Companies
{
    [Service]
    public class UpdateCompany
    {
        private ICompanyRepository _companyRepository;

        public UpdateCompany(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
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
            var product = _companyRepository.GetCompany(request.Id);
            product.Name = request.Name;           
            _companyRepository.Update(product);
            int i = await _companyRepository.Save();
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
