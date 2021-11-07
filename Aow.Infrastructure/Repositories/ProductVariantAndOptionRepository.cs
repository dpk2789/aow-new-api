using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class ProductVariantAndOptionRepository : RepositoryBase<ProductVariantProductAttributeOption>, IProductVariantAndOptionRepository
    {
        public ProductVariantAndOptionRepository(AowContext repositoryContext)
          : base(repositoryContext)
        {
        }

        public ProductVariantProductAttributeOption GetProductVarientsWithOption(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public Task<PagedList<ProductVariantProductAttributeOption>> GetProductVarientsWithOptions(PagingParameters ownerParameters, Guid varientId)
        {
            return Task.FromResult(PagedList<ProductVariantProductAttributeOption>.ToPagedList(FindAll().Where(x => x.ProductVariantId == varientId).Include(x => x.ProductVariant),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }
}
