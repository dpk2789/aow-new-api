using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.ProductAttribute
{
    [Service]
    public class DeleteProductAttibute
    {
        private IRepositoryWrapper _repoWrapper;
        public DeleteProductAttibute(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class DeleteProductAttibuteResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
        }

        public async Task<DeleteProductAttibuteResponse> Do(Guid id)
        {
            try
            {
                var attribute = _repoWrapper.ProductAttributeRepo.GetProductAttribute(id);
                if (attribute == null)
                {
                    return null;
                }
                if (attribute.ProductAttributeOptions != null)
                {
                    foreach (var option in attribute.ProductAttributeOptions)
                    {
                        _repoWrapper.ProductAttributeOptionRepo.Delete(option);
                    }
                }

                _repoWrapper.ProductAttributeRepo.Delete(attribute);
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new DeleteProductAttibuteResponse
                    {
                        Message = "Error Deleting",
                        Success = false
                    };
                }
                else
                {
                    return new DeleteProductAttibuteResponse
                    {
                        Message = "Attribute Deleted",
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new DeleteProductAttibuteResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }

        }
    }
}
