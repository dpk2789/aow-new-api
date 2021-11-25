using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IManufacturingVarientsRepository : IRepositoryBase<ManufacturingVarients>
    {
        Task<PagedList<ManufacturingVarients>> GetInputVarients(PagingParameters ownerParameters, Guid varientId);
        ManufacturingVarients GetInputVarient(Guid Id);
    }
}
