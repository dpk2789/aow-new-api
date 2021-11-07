using Aow.Infrastructure.IRepositories;
using System;

namespace Aow.Services.ProductVariants
{
    public class UpdateProductVariant
    {
        private IRepositoryWrapper _repoWrapper;
        public UpdateProductVariant(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class UpdateProductVariantRequest
        {
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
    }
}
