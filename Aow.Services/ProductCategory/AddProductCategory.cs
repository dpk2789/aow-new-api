using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.ProductCategory
{
    [Service]
    public class AddProductCategory
    {
        private IRepositoryWrapper _repoWrapper;
        public AddProductCategory(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class AddProductCategoryRequest
        {
            public string Name { get; set; }          
            public string CompanyId { get; set; }
        }
        public class AddProductCategoryResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<AddProductCategoryResponse> Do(AddProductCategoryRequest request)
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
                    return new AddProductCategoryResponse
                    {
                        Name = request.Name,
                        Success = false
                    };
                }
                else
                {
                    return new AddProductCategoryResponse
                    {
                        Id = financialYear.Id,
                        Name = request.Name,
                        Success = true,
                        Description = "Product Category SuccessFully Added"
                    };
                }
            }
            catch (Exception ex)
            {
                return new AddProductCategoryResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
