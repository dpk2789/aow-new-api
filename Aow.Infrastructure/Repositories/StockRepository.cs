using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
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

        public Task<PagedList<Stock>> GetStocks(PagingParameters ownerParameters, Guid companyId)
        {
            return Task.FromResult(PagedList<Stock>.ToPagedList(FindAll().Include(x => x.Product.ProductCategory).Include(x =>x.StockProductVariants).
                Where(x => x.Product.ProductCategory.CompanyId == companyId),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }
}
