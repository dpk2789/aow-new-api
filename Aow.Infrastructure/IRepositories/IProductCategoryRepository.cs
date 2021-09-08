using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IProductCategoryRepository : IRepositoryBase<ProductCategory>
    {
        ProductCategory GetProductCategory(Guid Id);
        Task<PagedList<ProductCategory>> GetProductCategories(PagingParameters ownerParameters, Guid cmpId);
    }
}
