using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IInputRepository :IRepositoryBase<InputVarients>
    {
        Task<PagedList<InputVarients>> GetInputVarients(PagingParameters ownerParameters, Guid companyId);
        InputVarients GetInputVarient(Guid Id);
    }
}
