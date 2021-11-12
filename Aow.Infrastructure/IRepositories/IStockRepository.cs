using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IStockRepository : IRepositoryBase<Stock>
    {
        Task<PagedList<Stock>> GetStocks(PagingParameters ownerParameters, Guid companyId);     
        Stock GetStock(Guid Id);
    }
}
