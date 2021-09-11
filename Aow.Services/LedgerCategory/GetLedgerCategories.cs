using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.LedgerCategory
{
    public  class GetLedgerCategories
    {
        private IRepositoryWrapper _repoWrapper;
        public GetLedgerCategories(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetProductCategoriesResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }  
        }

        public IEnumerable<GetProductCategoriesResponse> Do(PagingParameters pagingParameters, Guid companyId)
        {
            var user = _repoWrapper.CompanyRepo.GetCompany(companyId);
            if (user == null)
            {
                return null;
            };
            var list = _repoWrapper.ProductCategoryRepo.GetProductCategories(pagingParameters, companyId).GetAwaiter().GetResult();
            var newList = list.Select(x => new GetProductCategoriesResponse
            {
                Id = x.Id,
                Name = x.Name,
            });

            return newList;
        }
    }
}
