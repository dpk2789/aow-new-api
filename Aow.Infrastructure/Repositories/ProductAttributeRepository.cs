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
    public class ProductAttributeRepository : RepositoryBase<ProductAttribute>, IProductAttributeRepository
    {
        public ProductAttributeRepository(AowContext repositoryContext)
         : base(repositoryContext)
        {
        }

        public ProductAttribute GetProductAttribute(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).Include(x => x.ProductCategory).Include(x => x.ProductAttributeOptions).FirstOrDefault();
        }

        public Task<PagedList<ProductAttribute>> GetProductAttributes(PagingParameters ownerParameters, Guid categoryId)
        {
            return Task.FromResult(PagedList<ProductAttribute>.ToPagedList(FindAll().Where(x => x.ProductCategoryId == categoryId).
                Include(x => x.ProductCategory).Include(p => p.ProductAttributeOptions),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
        public Task<List<ProductAttribute>> GetProductAttributesByCatrgory(Guid categoryId)
        {
            return (FindAll().Where(x => x.ProductCategoryId == categoryId).
                Include(x => x.ProductCategory).Include(p => p.ProductAttributeOptions).ToListAsync());
        }

    }
}
