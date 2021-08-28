using Aow.Infrastructure.Paging;
using AowCore.Domain;
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
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public Task<PagedList<Product>> GetProducts(PagingParameters ownerParameters)
        {
            return Task.FromResult(PagedList<Product>.ToPagedList(FindAll().OrderBy(on => on.Name),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }

        public PagedList<Product> GetProductsWithStock(PagingParameters ownerParameters)
        {
            return PagedList<Product>.ToPagedList(FindAll(),
                                    ownerParameters.PageNumber, ownerParameters.PageSize);
        }
    }
}
