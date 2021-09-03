using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.FinancialYear
{
    [Service]
    public class DeleteFinancialYear
    {
        private IFinancialYearRepository _financialYearRepository;
        public DeleteFinancialYear(IFinancialYearRepository financialYearRepository)
        {
            _financialYearRepository = financialYearRepository;
        }
        public class DeleteFinancialYearResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
        }
        public async Task<DeleteFinancialYearResponse> Do(Guid id)
        {
            var company = _financialYearRepository.GetFinancialYear(id);
            if (company == null)
            {
                return null;
            }
            _financialYearRepository.Delete(company);
            int i = await _financialYearRepository.Save();
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
