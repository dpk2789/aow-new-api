using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aow.Services.ProductCategory
{
    public class GetItemVarientsSearchByCategory
    {
        private IRepositoryWrapper _repoWrapper;
        public GetItemVarientsSearchByCategory(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetProductCategoryResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid CompanyId { get; set; }
            public Guid? ParentCategoryId { get; set; }
            public IList<AttributesResponse> AttributesViewModel { get; set; }
            public IList<ProductVariantsResponse> ProductVariantsViewModel { get; set; }
        }
        public class ProductVariantsResponse
        {
            public Guid Id { get; set; }         
            public string Name { get; set; }  
            public IList<AttributesResponse> AttributesViewModels { get; set; }

        }
        public class AttributesResponse
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public IEnumerable<AttributesOptionsViewModel> AttributesOptionsViewModels { get; set; }
            public bool IsChecked { get; set; }
        }

        public class AttributesOptionsViewModel
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; }
        }
        public GetProductCategoryResponse Do(Guid id)
        {
            var retriveCategory = _repoWrapper.ProductCategoryRepo.GetProductCategory(id);          
            var getCompanyResponse = new GetProductCategoryResponse
            {
                Id = retriveCategory.Id,
                Name = retriveCategory.Name,

            };
            foreach (var category in retriveCategory.ProductAttributes)
            {
               
            }           

            return getCompanyResponse;
        }
    }
}
