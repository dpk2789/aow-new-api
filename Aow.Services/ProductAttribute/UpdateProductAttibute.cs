using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aow.Services.ProductAttribute
{
    [Service]
    public class UpdateProductAttibute
    {
        private IRepositoryWrapper _repoWrapper;
        public UpdateProductAttibute(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class UpdateProductAttributeRequest
        {
            public Guid Id { get; set; }
            public Guid ProductCategoryId { get; set; }
            public string Name { get; set; }
            public string OptionValue { get; set; }
            public IList<AttributesOptionsRequest> AttributesOptions { get; set; }
        }
        public class AttributesOptionsRequest
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public Guid AttributeId { get; set; }
            public bool IsChecked { get; set; }
        }
        public class UpdateProductAttributeResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<UpdateProductAttributeResponse> Do(UpdateProductAttributeRequest request)
        {
            try
            {
                var attribute = _repoWrapper.ProductAttributeRepo.GetProductAttribute(request.Id);
                if (attribute == null)
                {
                    return null;
                }
                Guid ProductId = Guid.NewGuid();
                attribute.Name = request.Name;

                //if (attribute.ProductAttributeOptions != null)
                //{
                //    foreach (var attributeOption in attribute.ProductAttributeOptions)
                //    {
                //        _repoWrapper.ProductAttributeOptionRepo.Delete(attributeOption);
                //    }
                //}
                if (request.AttributesOptions != null)
                {
                    foreach (var option in request.AttributesOptions)
                    {
                        var attributeOption = new Aow.Infrastructure.Domain.ProductAttributeOptions();
                        attributeOption.Id = Guid.NewGuid();
                        attributeOption.ProductAttributeId = request.Id;
                        attributeOption.Name = option.Name;
                        _repoWrapper.ProductAttributeOptionRepo.Create(attributeOption);
                    }
                }
                _repoWrapper.ProductAttributeRepo.Update(attribute);
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new UpdateProductAttributeResponse
                    {
                        Name = request.Name,
                        Success = false
                    };
                }
                else
                {
                    return new UpdateProductAttributeResponse
                    {
                        Id = attribute.Id,
                        Name = request.Name,
                        Success = true,
                        Description = "Product Attribute SuccessFully Added"
                    };
                }
            }
            catch (Exception ex)
            {
                return new UpdateProductAttributeResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
