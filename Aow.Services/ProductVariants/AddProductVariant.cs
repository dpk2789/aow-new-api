using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.ProductVariants
{
    [Service]
    public class AddProductVariant
    {
        private IRepositoryWrapper _repoWrapper;
        public AddProductVariant(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class AddProductVariantRequest
        {
            public string Name { get; set; }
            public Guid ProductId { get; set; }
            public Guid[] OptionsSelectedOnView { get; set; }
            
        }
        public class AddProductVariantResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<AddProductVariantResponse> Do(AddProductVariantRequest request)
        {
            try
            {
                Guid id = Guid.NewGuid();              
                var varient = new Aow.Infrastructure.Domain.ProductVariant
                {
                    Id = id,
                    Name = request.Name,
                    ProductId = request.ProductId,
                };

                _repoWrapper.ProductVarientRepo.Create(varient);
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
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new AddProductVariantResponse
                    {
                        Name = request.Name,
                        Success = false
                    };
                }
                else
                {
                    return new AddProductVariantResponse
                    {
                        Id = varient.Id,
                        Name = request.Name,
                        Success = true,
                        Description = "Product Varient SuccessFully Added"
                    };
                }
            }
            catch (Exception ex)
            {
                return new AddProductVariantResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
