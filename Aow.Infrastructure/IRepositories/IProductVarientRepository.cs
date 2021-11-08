using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IProductVarientRepository : IRepositoryBase<ProductVariant>
    {
        Task<PagedList<ProductVariant>> GetProductVarients(PagingParameters ownerParameters, Guid productId);
        Task<List<ProductVariant>> GetAllProductVarients(Guid productId);
        ProductVariant GetProductVariant(Guid Id);
    }
}
