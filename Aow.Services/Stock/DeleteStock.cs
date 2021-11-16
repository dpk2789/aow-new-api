using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Stock
{
    [Service]
    public class DeleteStock
    {
        private IRepositoryWrapper _repoWrapper;
        public DeleteStock(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class DeleteStockResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
        }

        public async Task<DeleteStockResponse> Do(Guid id)
        {
            try
            {
                var product = _repoWrapper.ProductRepo.GetProduct(id);
                if (product == null)
                {
                    return null;
                }
                _repoWrapper.ProductRepo.Delete(product);
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new DeleteStockResponse
                    {
                        Message = "Error Deleting",
                        Success = false
                    };
                }
                else
                {
                    return new DeleteStockResponse
                    {
                        Message = "Product Deleted",
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new DeleteStockResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }

        }
    }
}
