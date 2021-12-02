using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IManufacturingItemRepository : IRepositoryBase<ManufactureItem>
    {
        Task<PagedList<ManufactureItem>> GetManufacturingItems(PagingParameters ownerParameters, Guid manufactureId);
        ManufactureItem GetManufacturingItem(Guid Id);
    }
}
