using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IProductAttributeRepository : IRepositoryBase<ProductAttribute>
    {
        ProductAttribute GetProductAttribute(Guid Id);
        Task<PagedList<ProductAttribute>> GetProductAttributes(PagingParameters ownerParameters, Guid cmpId);
    }
}
