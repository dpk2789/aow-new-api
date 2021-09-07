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
            public Guid CompanyId { get; set; }          
        }

        public GetProductCategoryResponse Do(Guid id)
        {
            var financialYear = _repoWrapper.ProductCategoryRepo.GetProductCategory(id);
            GetProductCategoryResponse getCompanyResponse = new GetProductCategoryResponse
            {
                Id = financialYear.Id,
                CompanyId = financialYear.Id,
                Name = financialYear.Name,      
            };

            return getCompanyResponse;
        }

    }
}
