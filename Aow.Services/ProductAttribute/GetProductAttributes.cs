using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.ProductAttribute
{
    [Service]
    public class GetProductAttributes
    {
        private IRepositoryWrapper _repoWrapper;
        public GetProductAttributes(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetProductAttributesResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string ParentCategoryName { get; set; }
        }

        public IEnumerable<GetProductAttributesResponse> Do(PagingParameters pagingParameters, Guid categoryId)
        {
            var list = _repoWrapper.ProductAttributeRepo.GetProductAttributes(pagingParameters, categoryId).GetAwaiter().GetResult();
            var newList = list.Select(x => new GetProductAttributesResponse
            {
                Id = x.Id,
                Name = x.Name,
                ParentCategoryName = x.ProductCategory.Name
            });

            return newList;
        }
    }
}
