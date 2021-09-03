using Aow.Infrastructure.Paging;
using AowCore.Domain;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<PagedList<Product>> GetProducts(PagingParameters ownerParameters);
        PagedList<Product> GetProductsWithStock(PagingParameters ownerParameters);
        Product GetProduct(Guid Id);
    }
}
