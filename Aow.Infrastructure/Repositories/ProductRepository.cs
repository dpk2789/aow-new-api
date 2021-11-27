using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AowContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public Product GetProduct(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).Include(x => x.ProductVariants).FirstOrDefault();
        }

        public Task<PagedList<Product>> GetProducts(PagingParameters ownerParameters, Guid companyId)
        {
            return Task.FromResult(PagedList<Product>.ToPagedList(FindAll().Where(x => x.ProductCategory.CompanyId == companyId).Include(x => x.ProductCategory).OrderBy(on => on.Name),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }

        public PagedList<Product> GetProductsWithStock(PagingParameters ownerParameters)
        {
            return PagedList<Product>.ToPagedList(FindAll(),
                                    ownerParameters.PageNumber, ownerParameters.PageSize);
        }
    }
}
