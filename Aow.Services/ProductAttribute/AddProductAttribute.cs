using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.ProductAttribute
{
    [Service]
    public class AddProductAttribute
    {
        private IRepositoryWrapper _repoWrapper;
        public AddProductAttribute(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class AddProductAttributeRequest
        {
            public string Name { get; set; }
            public string CompanyId { get; set; }
        }
        public class AddProductAttributeResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<AddProductAttributeResponse> Do(AddProductAttributeRequest request)
        {
            try
            {
                Guid ProductId = Guid.NewGuid();
                Guid cmpId = Guid.Parse(request.CompanyId);
                var financialYear = new Aow.Infrastructure.Domain.ProductCategory
                {
                    Id = ProductId,
                    Name = request.Name,
                    CompanyId = cmpId,
                };

                _repoWrapper.ProductCategoryRepo.Create(financialYear);
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new AddProductAttributeResponse
                    {
                        Name = request.Name,
                        Success = false
                    };
                }
                else
                {
                    return new AddProductAttributeResponse
                    {
                        Id = financialYear.Id,
                        Name = request.Name,
                        Success = true,
                        Description = "Product Attribute SuccessFully Added"
                    };
                }
            }
            catch (Exception ex)
            {
                return new AddProductAttributeResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
