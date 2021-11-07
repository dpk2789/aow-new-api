using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IProductVariantAndOptionRepository : IRepositoryBase<ProductVariantProductAttributeOption>
    {
        Task<PagedList<ProductVariantProductAttributeOption>> GetProductVarientsWithOptions(PagingParameters ownerParameters, Guid productId);
        ProductVariantProductAttributeOption GetProductVarientsWithOption(Guid Id);
        Task<List<ProductVariantProductAttributeOption>> GetVarientsWithOptionsByVarient(Guid varientId);
    }
}
