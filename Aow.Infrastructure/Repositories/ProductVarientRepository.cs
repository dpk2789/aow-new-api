using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class ProductVarientRepository : RepositoryBase<ProductVariant>, IProductVarientRepository
    {
        public ProductVarientRepository(AowContext repositoryContext)
           : base(repositoryContext)
        {
        }

        public ProductVariant GetProductVariant(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public Task<PagedList<ProductVariant>> GetProductVarients(PagingParameters ownerParameters, Guid productId)
        {
            return Task.FromResult(PagedList<ProductVariant>.ToPagedList(FindAll().Where(x => x.ProductId == productId).Include(x => x.Products).OrderBy(on => on.Name),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
      
    }
}
