using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IManufactureRepository :IRepositoryBase<Manufacture>
    {
        Task<PagedList<Manufacture>> GetManufactures(PagingParameters ownerParameters, Guid companyId);
        Manufacture GetManufacture(Guid Id);
    }
}
