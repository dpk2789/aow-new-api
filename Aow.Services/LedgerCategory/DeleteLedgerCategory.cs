using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.LedgerCategory
{
    [Service]
    public class DeleteLedgerCategory
    {
        private IRepositoryWrapper _repoWrapper;
        public DeleteLedgerCategory(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class DeleteLedgerCategoryResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
        }

        public async Task<DeleteLedgerCategoryResponse> Do(Guid id)
        {
            var category = _repoWrapper.LedgerCategoryRepositoryRepo.GetLedgerCategory(id);
            if (category == null)
            {
                return null;
            }
            _repoWrapper.LedgerCategoryRepositoryRepo.Delete(category);
            int i = await _repoWrapper.SaveNew();
            if (i <= 0)
            {
                return new DeleteLedgerCategoryResponse
                {
                    Message = "Error Deleting",
                    Success = false
                };
            }
            else
            {
                return new DeleteLedgerCategoryResponse
                {
                    Message = "Ledger Category Deleted",
                    Success = true
                };
            }
        }
    }
}
