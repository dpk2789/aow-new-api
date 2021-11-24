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
    public class ProductVarientRepository : RepositoryBase<ProductVariant>, IProductVarientRepository
    {
        public ProductVarientRepository(AowContext repositoryContext)
           : base(repositoryContext)
        {
        }

        public ProductVariant GetProductVariant(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).Include(x => x.Products).
                Include(x => x.Products.ProductCategory).Include(x => x.ProductVariantProductAttributeOptions).FirstOrDefault();
        }

        public Task<PagedList<ProductVariant>> GetProductVarients(PagingParameters ownerParameters, Guid productId)
        {
            return Task.FromResult(PagedList<ProductVariant>.ToPagedList(FindAll().Where(x => x.ProductId == productId).
                Include(x => x.Products).OrderBy(on => on.Name),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }

        public Task<List<ProductVariant>> GetAllProductVarients(Guid productId)
        {
            return (FindAll().Where(x => x.ProductId == productId).Include(x => x.Products).ToListAsync());
        }

        public Task<List<ProductVariant>> GetAllProductVarientsByCompany(Guid companyId)
        {
            return (FindAll().Include(x => x.Products.ProductCategory).Where(x => x.Products.ProductCategory.CompanyId == companyId).ToListAsync());
        }

    }
}
