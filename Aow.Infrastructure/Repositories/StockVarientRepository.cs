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
    public class StockVarientRepository : RepositoryBase<StockProductVariant>, IStockVarientRepository
    {
        public StockVarientRepository(AowContext repositoryContext)
           : base(repositoryContext)
        {
        }

        public StockProductVariant GetStockVarient(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }
        public StockProductVariant GetStockVarientByVoucherVarient(Guid Id)
        {
            return FindByCondition(x => x.VoucherItemVarientId == Id).FirstOrDefault();
        }
        public async Task<List<StockProductVariant>> GetStockVarientByCompany(Guid companyId)
        {
            var result = await FindAll().Include(x => x.Stock.Product.ProductCategory).Include(x => x.ProductVariant).
                Where(x => x.Stock.Product.ProductCategory.CompanyId == companyId).ToListAsync();
            return result;
        }       
        public Task<PagedList<StockProductVariant>> GetStockVarients(PagingParameters ownerParameters, Guid stockId)
        {
            return Task.FromResult(PagedList<StockProductVariant>.ToPagedList(FindAll().Include(x => x.Stock).
                Where(x => x.StockId == stockId), ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }
}
