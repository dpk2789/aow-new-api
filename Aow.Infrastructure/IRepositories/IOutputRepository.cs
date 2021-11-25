using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IOutputRepository : IRepositoryBase<OutputVarients>
    {
        Task<PagedList<OutputVarients>> GetOutputVarients(PagingParameters ownerParameters, Guid companyId);
        OutputVarients GetOutputVarient(Guid Id);
    }
}
