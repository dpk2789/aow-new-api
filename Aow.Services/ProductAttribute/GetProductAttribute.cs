using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.ProductAttribute
{
    [Service]
    public class GetProductAttribute
    {
        private IRepositoryWrapper _repoWrapper;
        public GetProductAttribute(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetProductAttributeResponse
        {
            public Guid Id { get; set; }
            public Guid CategoryId { get; set; }
            public string Name { get; set; }
            public string ParentCategoryName { get; set; }
            public IList<GetAttributesOptionsResponse> AttributesOptions { get; set; }
        }
        public class GetAttributesOptionsResponse
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public Guid AttributeId { get; set; }
            public bool IsChecked { get; set; }
        }
        public GetProductAttributeResponse Do(Guid Id)
        {
            var attribute = _repoWrapper.ProductAttributeRepo.GetProductAttribute(Id);
            if (attribute == null)
            {
                return null;
            }
            GetProductAttributeResponse getProductAttributeResponse = new GetProductAttributeResponse();
            getProductAttributeResponse.Id = attribute.Id;
            getProductAttributeResponse.ParentCategoryName = attribute.ProductCategory.Name;
            getProductAttributeResponse.CategoryId = attribute.ProductCategory.Id;
            getProductAttributeResponse.Name = attribute.Name;
            if (attribute.ProductAttributeOptions != null)
            {
                var optionList = new List<GetAttributesOptionsResponse>();
                foreach (var option in attribute.ProductAttributeOptions.OrderBy(x => x.Name))
                {
                    GetAttributesOptionsResponse getAttributesOptionsResponse = new GetAttributesOptionsResponse();
                    getAttributesOptionsResponse.Id = option.Id;
                    getAttributesOptionsResponse.Name = option.Name;
                    optionList.Add(getAttributesOptionsResponse);
                }
                getProductAttributeResponse.AttributesOptions = optionList;
            }


            return getProductAttributeResponse;
        }
    }
}
