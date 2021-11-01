using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.ProductCategory
{
    [Service]
    public class GetProductCategories
    {
        private IRepositoryWrapper _repoWrapper;
        public GetProductCategories(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetProductCategoriesResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string ParentCategoryName { get; set; }
            public Guid? ParentCategoryId { get; set; }

        }

        public IEnumerable<GetProductCategoriesResponse> Do(PagingParameters pagingParameters, Guid companyId)
        {
            var user = _repoWrapper.CompanyRepo.GetCompany(companyId);
            if (user == null)
            {
                return null;
            };
            var categories = new List<GetProductCategoriesResponse>();
            var list = _repoWrapper.ProductCategoryRepo.GetProductCategories(pagingParameters, companyId).GetAwaiter().GetResult();
            var newList = list.Where(x => x.Type != "Sundry Item");
            foreach (var item in newList)
            {
                var getProductCategoriesResponse = new GetProductCategoriesResponse();
                getProductCategoriesResponse.Id = item.Id;
                getProductCategoriesResponse.Name = item.Name;
                if (item.ParentCategoryId != null)
                {
                    var category = list.FirstOrDefault(x => x.Id == item.ParentCategoryId);
                    getProductCategoriesResponse.ParentCategoryName = category.Name;
                }
                categories.Add(getProductCategoriesResponse);
            }

            return categories;
        }
    }
}
