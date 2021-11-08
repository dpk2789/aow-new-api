using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.ProductVariants
{
    [Service]
    public class GetProductVariants
    {
        private IRepositoryWrapper _repoWrapper;
        public GetProductVariants(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetProductVariantsResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }           
            public Guid? ProductId { get; set; }
  
        }

        public IEnumerable<GetProductVariantsResponse> Do(PagingParameters pagingParameters, Guid productId)
        {
            var list = _repoWrapper.ProductVarientRepo.GetProductVarients(pagingParameters, productId).GetAwaiter().GetResult();
            var newList = list.Select(x => new GetProductVariantsResponse
            {
                Id = x.Id,
                ProductId = x.ProductId,               
                Name = x.Name,             
            });

            return newList;
        }
    }
}
