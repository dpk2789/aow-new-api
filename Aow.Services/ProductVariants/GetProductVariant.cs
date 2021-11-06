using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;

namespace Aow.Services.ProductVariants
{
    [Service]
    public class GetProductVariant
    {
        private IRepositoryWrapper _repoWrapper;
        public GetProductVariant(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetProductVariantResponse
        {
            public Guid Id { get; set; }
            public Guid ProductId { get; set; }
            public string Name { get; set; }
            public string ProductName { get; set; }
            public IList<ProductVariantProductAttributeOptionResponse> AttributesOptions { get; set; }
        }
        public class ProductVariantProductAttributeOptionResponse
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }            
            public Guid? ProductAttributeOptionsId { get; set; }
        }

        public GetProductVariantResponse Do(Guid Id)
        {
            var varient = _repoWrapper.ProductVarientRepo.GetProductVariant(Id);
            if (varient == null)
            {
                return null;
            }
            GetProductVariantResponse getProductAttributeResponse = new GetProductVariantResponse
            {
                Id = varient.Id,
                ProductName = varient.Products.Name,
                ProductId = varient.Products.Id,
                Name = varient.Name
            };
            if (varient.ProductVariantProductAttributeOptions != null)
            {
                var optionList = new List<ProductVariantProductAttributeOptionResponse>();
                foreach (var option in varient.ProductVariantProductAttributeOptions)
                {
                    ProductVariantProductAttributeOptionResponse getAttributesOptionsResponse = new ProductVariantProductAttributeOptionResponse();
                    getAttributesOptionsResponse.Id = option.Id;
                    getAttributesOptionsResponse.Name = option.ProductAttributeOptions.Name;
                    optionList.Add(getAttributesOptionsResponse);
                }
                getProductAttributeResponse.AttributesOptions = optionList;
            }

            return getProductAttributeResponse;
        }
    }
}
