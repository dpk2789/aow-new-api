using Aow.Infrastructure.IRepositories;
using System;

namespace Aow.Services.LedgerCategory
{
    public class GetLedgerCategory
    {
        private IRepositoryWrapper _repoWrapper;
        public GetLedgerCategory(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetLedgerCategoryResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid CompanyId { get; set; }
        }

        public GetLedgerCategoryResponse Do(Guid id)
        {
            var financialYear = _repoWrapper.ProductCategoryRepo.GetProductCategory(id);
            GetLedgerCategoryResponse getCompanyResponse = new GetLedgerCategoryResponse
            {
                Id = financialYear.Id,
                CompanyId = financialYear.Id,
                Name = financialYear.Name,
            };

            return getCompanyResponse;
        }
    }
}
