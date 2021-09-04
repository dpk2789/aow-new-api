using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.FinancialYear
{
    [Service]
    public class DeleteFinancialYear
    {
        private IRepositoryWrapper _repoWrapper;
        public DeleteFinancialYear(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class DeleteFinancialYearResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
        }
        public async Task<DeleteFinancialYearResponse> Do(Guid id)
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
                return new DeleteFinancialYearResponse
                {
                    Message = "Error Deleting",
                    Success = false
                };
            }
            else
            {
                return new DeleteFinancialYearResponse
                {
                    Message = "FinancialYear Deleted",
                    Success = true
                };
            }
        }
    }
}
