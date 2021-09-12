using Aow.Infrastructure.IRepositories;
using System;

namespace Aow.Services.Ledger
{
    [Service]
    public class GetLedger
    {
        private IRepositoryWrapper _repoWrapper;
        public GetLedger(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetLedgerResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid CompanyId { get; set; }
        }

        public GetLedgerResponse Do(Guid id)
        {
            var financialYear = _repoWrapper.ProductCategoryRepo.GetProductCategory(id);
            GetLedgerResponse getCompanyResponse = new GetLedgerResponse
            {
                Id = financialYear.Id,
                CompanyId = financialYear.Id,
                Name = financialYear.Name,
            };

            return getCompanyResponse;
        }

    }
}
