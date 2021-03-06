using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IProductAttributeOptionRepository : IRepositoryBase<ProductAttributeOptions>
    {
        ProductAttributeOptions GetProductAttributeOption(Guid Id);
        Task<PagedList<ProductAttributeOptions>> GetProductAttributeOptions(PagingParameters ownerParameters, Guid cmpId);
    }
}
