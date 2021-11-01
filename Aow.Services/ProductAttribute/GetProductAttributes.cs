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
            public DateTime? Start { get; set; }
            public DateTime? End { get; set; }
            public bool IsActive { get; set; }
            public bool? IsLocked { get; set; }
        }

        public IEnumerable<GetProductAttributesResponse> Do(PagingParameters pagingParameters, Guid companyId)
        {
            var user = _repoWrapper.CompanyRepo.GetCompany(companyId);
            if (user == null)
            {
                return null;
            };
            var list = _repoWrapper.ProductCategoryRepo.GetProductCategories(pagingParameters, companyId).GetAwaiter().GetResult();
            var newList = list.Where(x => x.Type != "Sundry Item").Select(x => new GetProductAttributesResponse
            {
                Id = x.Id,
                Name = x.Name,
            });

            return newList;
        }
    }
}
