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
            public Guid? ParentCategoryId { get; set; }
        }

        public GetProductCategoryResponse Do(Guid id)
        {
            var category = _repoWrapper.ProductCategoryRepo.GetProductCategory(id);
            var getCompanyResponse = new GetProductCategoryResponse
            {
                Id = category.Id,
                CompanyId = category.CompanyId,
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId
            };

            return getCompanyResponse;
        }

    }
}
