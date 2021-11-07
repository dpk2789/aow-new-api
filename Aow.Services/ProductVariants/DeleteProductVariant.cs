using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.ProductVariants
{
    [Service]
    public class DeleteProductVariant
    {
        private IRepositoryWrapper _repoWrapper;
        public DeleteProductVariant(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class DeleteProductVariantResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
        }
        public async Task<DeleteProductVariantResponse> Do(Guid id)
        {
            try
            {
                var varient = _repoWrapper.ProductVarientRepo.GetProductVariant(id);
                if (varient == null)
                {
                    return null;
                }
                _repoWrapper.ProductVarientRepo.Delete(varient);
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new DeleteProductVariantResponse
                    {
                        Message = "Error Deleting",
                        Success = false
                    };
                }
                else
                {
                    return new DeleteProductVariantResponse
                    {
                        Message = "Varient Deleted",
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new DeleteProductVariantResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }

        }
    }
}
