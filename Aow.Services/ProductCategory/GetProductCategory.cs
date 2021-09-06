using Aow.Infrastructure.IRepositories;
using System;

namespace Aow.Services.ProductCategory
{
    [Service]
    public class GetProductCategory
    {
        private IRepositoryWrapper _repoWrapper;
        public GetProductCategory(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetProductCategoryResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public DateTime? Start { get; set; }
            public DateTime? End { get; set; }
            public DateTime CreatedDate { get; set; }
            public bool IsActive { get; set; }
            public bool? IsLocked { get; set; }
            public Guid CompanyId { get; set; }
            public bool Success { get; set; }
        }

        public GetProductCategoryResponse Do(Guid id)
        {
            var financialYear = _repoWrapper.FinancialYearRepo.GetFinancialYear(id);
            GetProductCategoryResponse getCompanyResponse = new GetProductCategoryResponse
            {
                Id = financialYear.Id,
                CompanyId = financialYear.Id,
                Name = financialYear.Name,
                Start = financialYear.Start,
                End = financialYear.End
            };

            return getCompanyResponse;
        }

    }
}
