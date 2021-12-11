using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.ProductCategory
{
    [Service]
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
            public IList<CategoryAttributesResponse> AttributesViewModel { get; set; }
            public IList<ProductVariantsResponse> ProductVariantsViewModel { get; set; }
        }
        public class CategoryAttributesResponse
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public IEnumerable<AttributesOptionsResponse> AttributesOptionsViewModels { get; set; }
            public bool IsChecked { get; set; }
        }
        public class AttributesOptionsResponse
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; }
        }
        public class ProductVariantsResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public IList<CategoryAttributesResponse> AttributesViewModels { get; set; }
        }
        public GetProductCategoryResponse Do(Guid id)
        {
            var retriveCategory = _repoWrapper.ProductCategoryRepo.GetProductCategoryForSearch(id);
            var getCompanyResponse = new GetProductCategoryResponse
            {
                Id = retriveCategory.Id,
                Name = retriveCategory.Name,
            };
            getCompanyResponse.AttributesViewModel = retriveCategory.ProductAttributes.Select(tag => new CategoryAttributesResponse
            {
                Id = tag.Id,
                Name = tag.Name,
                AttributesOptionsViewModels = tag.ProductAttributeOptions.Select(x => new AttributesOptionsResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsChecked = false,
                }),
                // IsChecked = false,
            }).ToList();
            var productVariantsViewModelList = new List<ProductVariantsResponse>();
            foreach (var product in retriveCategory.Products)
            {
                foreach (var variant in product.ProductVariants)
                {
                    ProductVariantsResponse viewModel = new ProductVariantsResponse();
                    viewModel.Id = variant.Id;
                    viewModel.Name = variant.Name;                  
                    productVariantsViewModelList.Add(viewModel);
                }
            }
            getCompanyResponse.ProductVariantsViewModel = productVariantsViewModelList;
            return getCompanyResponse;
        }
    }
}
