using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.ProductVariants
{
    [Service]
    public class UpdateProductVariant
    {
        private IRepositoryWrapper _repoWrapper;
        public UpdateProductVariant(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class UpdateProductVariantRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid ProductId { get; set; }
            public Guid[] OptionsSelectedOnView { get; set; }

        }
        public class UpdateProductVariantResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<UpdateProductVariantResponse> Do(UpdateProductVariantRequest request)
        {
            try
            {
                var varient = _repoWrapper.ProductVarientRepo.GetProductVariant(request.Id);
                varient.Name = request.Name;             

                foreach (var option in varient.ProductVariantProductAttributeOptions)
                {                    
                    _repoWrapper.ProductVariantAndOptionRepo.Delete(option);
                }
                if (request.OptionsSelectedOnView != null)
                {
                    foreach (var option in request.OptionsSelectedOnView)
                    {
                        var optionVarient = new Aow.Infrastructure.Domain.ProductVariantProductAttributeOption();
                        optionVarient.Id = Guid.NewGuid();
                        optionVarient.ProductAttributeOptionsId = option;
                        optionVarient.ProductVariantId = varient.Id;
                        _repoWrapper.ProductVariantAndOptionRepo.Create(optionVarient);
                    }
                }

                _repoWrapper.ProductVarientRepo.Update(varient);
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new UpdateProductVariantResponse
                    {
                        Name = request.Name,
                        Success = false
                    };
                }
                else
                {
                    return new UpdateProductVariantResponse
                    {
                        Id = varient.Id,
                        Name = request.Name,
                        Success = true,
                        Description = "Product Varient SuccessFully Updated"
                    };
                }
            }
            catch (Exception ex)
            {
                return new UpdateProductVariantResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
