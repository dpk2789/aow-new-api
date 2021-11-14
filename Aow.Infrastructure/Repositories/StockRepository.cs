using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public async Task<List<Stock>> GetStockByVoucherItemId(Guid VoucherItemId)
        {
            var result = await FindAll().Where(x => x.VoucherItemId == VoucherItemId).Include(x => x.StockProductVariants).ToListAsync();
            return result;
        }
        public Task<PagedList<Stock>> GetStocks(PagingParameters ownerParameters, Guid companyId)
        {
            return Task.FromResult(PagedList<Stock>.ToPagedList(FindAll().Include(x => x.Product.ProductCategory).
                Include(x => x.StockProductVariants).ThenInclude(x => x.ProductVariant).
                Where(x => x.Product.ProductCategory.CompanyId == companyId),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }
}
