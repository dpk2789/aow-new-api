using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories 
{
    public class StockRepository : RepositoryBase<Stock>, IStockRepository
    {
        public StockRepository(AowContext repositoryContext)
           : base(repositoryContext)
        {
        }

        public Stock GetStock(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }           

        public Task<PagedList<Stock>> GetStocks(PagingParameters ownerParameters, Guid VoucherItemId)
        {
            return Task.FromResult(PagedList<Stock>.ToPagedList(FindAll().Where(x => x.VoucherItemId == VoucherItemId).OrderBy(on => on.Name),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }
}
