using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Companies
{
    [Service]
    public class AddCompany
    {
        private ICompanyRepository _companyRepository;

        public AddCompany(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public class CreateRequest
        {
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
            Guid ProductId = Guid.NewGuid();
            var product = new Company
            {
                Id = ProductId,
                Name = request.Name,
            };

            _companyRepository.Create(product);
            int i = await _companyRepository.Save();
            if (i <= 0)
            {
                throw new Exception("Failed to create product");
            }

            return new CreateResponse
            {
                Id = product.Id,
                Name = product.Name
            };
        }
    }
}
