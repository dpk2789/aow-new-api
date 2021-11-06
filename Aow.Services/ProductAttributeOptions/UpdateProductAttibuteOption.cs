using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.ProductAttributeOptions
{
    [Service]
    public class UpdateProductAttibuteOption
    {
        private IRepositoryWrapper _repoWrapper;
        public UpdateProductAttibuteOption(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class UpdateAttributesOptionsRequest
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public Guid AttributeId { get; set; }
            public bool IsChecked { get; set; }
        }
        public class UpdateProductAttributeOptionResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<UpdateProductAttributeOptionResponse> Do(UpdateAttributesOptionsRequest request)
        {
            try
            {
                var option = _repoWrapper.ProductAttributeOptionRepo.GetProductAttributeOption(request.Id.Value);
                if (option == null)
                {
                    return null;
                }
                option.Name = request.Name;
                _repoWrapper.ProductAttributeOptionRepo.Update(option);
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new UpdateProductAttributeOptionResponse
                    {
                        Name = request.Name,
                        Success = false
                    };
                }
                else
                {
                    return new UpdateProductAttributeOptionResponse
                    {
                        Id = option.Id,
                        Name = request.Name,
                        Success = true,
                        Description = "Attribute Option SuccessFully Added"
                    };
                }
            }
            catch (Exception ex)
            {
                return new UpdateProductAttributeOptionResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
