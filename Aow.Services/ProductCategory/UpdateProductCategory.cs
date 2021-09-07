using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.ProductCategory
{
    [Service]
    public class UpdateProductCategory
    {
        private IRepositoryWrapper _repoWrapper;
        public UpdateProductCategory(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class UpdateProductCategoryRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string CompanyId { get; set; }
        }
        public class UpdateProductCategoryResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<UpdateProductCategoryResponse> Do(UpdateProductCategoryRequest request)
        {
            try
            {
                var category = _repoWrapper.ProductCategoryRepo.GetProductCategory(request.Id);
                if (category == null)
                {
                    return null;
                }
                category.Name = request.Name;
                _repoWrapper.ProductCategoryRepo.Update(category);

                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new UpdateProductCategoryResponse
                    {
                        Name = request.Name,
                        Success = false,
                        Description = "Some Error Occur"
                    };
                }
                else
                {
                    return new UpdateProductCategoryResponse
                    {
                        Id = category.Id,
                        Name = request.Name,
                        Success = true,
                        Description = "Successfully Updated Product Category!!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new UpdateProductCategoryResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
