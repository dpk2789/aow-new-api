using Aow.Infrastructure.IRepositories;
using System;

namespace Aow.Services.LedgerCategory
{
    [Service]
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
            var category = _repoWrapper.LedgerCategoryRepositoryRepo.GetLedgerCategory(id);
            GetLedgerCategoryResponse getCompanyResponse = new GetLedgerCategoryResponse
            {
                Id = category.Id,
                CompanyId = category.Id,
                Name = category.Name,
            };

            return getCompanyResponse;
        }
    }
}
