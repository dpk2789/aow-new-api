using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IStockVarientRepository : IRepositoryBase<StockProductVariant>
    {
        Task<PagedList<StockProductVariant>> GetStockVarients(PagingParameters ownerParameters, Guid stockId);
        Task<List<StockProductVariant>> GetStockVarientByCompany(Guid companyId);
        StockProductVariant GetStockVarient(Guid Id);
        StockProductVariant GetStockVarientByVoucherVarient(Guid Id);
    }
}
