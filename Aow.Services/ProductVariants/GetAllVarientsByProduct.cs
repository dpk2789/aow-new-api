using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.ProductVariants
{
    [Service]
    public class GetAllVarientsByProduct
    {
        private IRepositoryWrapper _repoWrapper;
        public GetAllVarientsByProduct(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetAllProductVariantsResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid? ProductId { get; set; }
        }

        public IEnumerable<GetAllProductVariantsResponse> Do(Guid productId)
        {
            var list = _repoWrapper.ProductVarientRepo.GetAllProductVarients(productId).GetAwaiter().GetResult();
            if (list == null)
            {
                return null;
            }
            var newList = list.Select(x => new GetAllProductVariantsResponse
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Name = x.Name,
            });

            return newList;
        }
    }
}
