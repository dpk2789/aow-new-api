using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Ledger
{
    public class DeleteLedger
    {
        private IRepositoryWrapper _repoWrapper;
        public DeleteLedger(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class DeleteLedgerResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
        }

        public async Task<DeleteLedgerResponse> Do(Guid id)
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
                return new DeleteLedgerResponse
                {
                    Message = "Error Deleting",
                    Success = false
                };
            }
            else
            {
                return new DeleteLedgerResponse
                {
                    Message = "Product Category Deleted",
                    Success = true
                };
            }
        }
    }
}
