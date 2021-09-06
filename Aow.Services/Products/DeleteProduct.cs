using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Products
{
    [Service]
    public class DeleteProduct
    {
        private IRepositoryWrapper _repoWrapper;
        public DeleteProduct(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class DeleteProductResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
        }

        public async Task<DeleteProductResponse> Do(Guid id)
        {
            try
            {
                var financialYear = _repoWrapper.FinancialYearRepo.GetFinancialYear(id);
                if (financialYear == null)
                {
                    return null;
                }
                _repoWrapper.FinancialYearRepo.Delete(financialYear);
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new DeleteProductResponse
                    {
                        Message = "Error Deleting",
                        Success = false
                    };
                }
                else
                {
                    return new DeleteProductResponse
                    {
                        Message = "FinancialYear Deleted",
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new DeleteProductResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }

        }
    }
}
