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
            public IList<GetVariantAttributesResponse> Attributes { get; set; }
        }
        public class GetVariantAttributesResponse
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public IEnumerable<GetVariantAttributesOptionsResponse> Options { get; set; }
            public bool IsChecked { get; set; }
        }

        public class GetVariantAttributesOptionsResponse
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; }
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
                Name = varient.Name,
                ProductId = varient.ProductId
            };
            if (varient.ProductVariantProductAttributeOptions != null)
            {
                var attviewModelList = new List<GetVariantAttributesResponse>();
                foreach (var attribute in varient.Products.ProductCategory.ProductAttributes)
                {
                    var optionList = new List<GetVariantAttributesOptionsResponse>();
                    GetVariantAttributesResponse getAttributesOptionsResponse = new GetVariantAttributesResponse();
                    foreach (var attributeOption in attribute.ProductAttributeOptions)
                    {
                        GetVariantAttributesOptionsResponse getVariantAttributesOptionsResponse = new GetVariantAttributesOptionsResponse();
                        optionList.Add(getVariantAttributesOptionsResponse);
                    }
                    attviewModelList.Add(getAttributesOptionsResponse);
                }

            }

            return getProductAttributeResponse;
        }
    }
}
