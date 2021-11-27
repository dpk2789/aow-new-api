using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<GetProductVariantResponse> Do(Guid Id)
        {
            var varient = _repoWrapper.ProductVarientRepo.GetProductVariant(Id);
            if (varient == null)
            {
                return null;
            }
            var getProductAttributeResponse = new GetProductVariantResponse
            {
                Id = varient.Id,
                Name = varient.Name,
                ProductId = varient.ProductId,
                ProductName = varient.Products.Name
            };
            var varientsInPvPat = await _repoWrapper.ProductVariantAndOptionRepo.GetVarientsWithOptionsByVarient(varient.Id);
            var getAttributes = await _repoWrapper.ProductAttributeRepo.GetProductAttributesByCatrgory(varient.Products.ProductCategoryId);
            var attviewModelList = new List<GetVariantAttributesResponse>();
            foreach (var attribute in getAttributes)
            {
                var optionList = new List<GetVariantAttributesOptionsResponse>();
                var attributeResponse = new GetVariantAttributesResponse();
                attributeResponse.Id = attribute.Id;
                attributeResponse.Name = attribute.Name;
                foreach (var attributeOption in attribute.ProductAttributeOptions)
                {
                    var optionsResponse = new GetVariantAttributesOptionsResponse();
                    optionsResponse.Id = attributeOption.Id;
                    optionsResponse.Name = attributeOption.Name;
                    if (varientsInPvPat.Any(x => x.ProductAttributeOptionsId == attributeOption.Id))
                    {
                        optionsResponse.IsChecked = true;
                    }
                    optionList.Add(optionsResponse);
                }
                attributeResponse.Options = optionList;
                attviewModelList.Add(attributeResponse);
            }
            getProductAttributeResponse.Attributes = attviewModelList;
            return getProductAttributeResponse;
        }
    }
}
