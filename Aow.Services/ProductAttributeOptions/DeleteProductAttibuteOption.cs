using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.ProductAttributeOptions
{
    [Service]
    public class DeleteProductAttibuteOption
    {
        private IRepositoryWrapper _repoWrapper;
        public DeleteProductAttibuteOption(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class DeleteProductAttibuteOptionResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
        }
        public async Task<DeleteProductAttibuteOptionResponse> Do(Guid id)
        {
            try
            {
                var option = _repoWrapper.ProductAttributeOptionRepo.GetProductAttributeOption(id);
                if (option == null)
                {
                    return null;
                }
                _repoWrapper.ProductAttributeOptionRepo.Delete(option);
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new DeleteProductAttibuteOptionResponse
                    {
                        Message = "Error Deleting",
                        Success = false
                    };
                }
                else
                {
                    return new DeleteProductAttibuteOptionResponse
                    {
                        Message = "Option Deleted",
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new DeleteProductAttibuteOptionResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }

        }
    }
}
