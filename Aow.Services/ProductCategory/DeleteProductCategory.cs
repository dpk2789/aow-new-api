using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.ProductCategory
{
    [Service]
    public class DeleteProductCategory
    {
        private IRepositoryWrapper _repoWrapper;
        public DeleteProductCategory(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class DeleteProductCategoryResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
        }

        public async Task<DeleteProductCategoryResponse> Do(Guid id)
        {
            try
            {
                var category = _repoWrapper.ProductCategoryRepo.GetProductCategory(id);
                if (category == null)
                {
                    return null;
                }
                _repoWrapper.ProductCategoryRepo.Delete(category);
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new DeleteProductCategoryResponse
                    {
                        Message = "Error Deleting",
                        Success = false
                    };
                }
                else
                {
                    return new DeleteProductCategoryResponse
                    {
                        Message = "Product Category Deleted",
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new DeleteProductCategoryResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
         
        }
    }
}
