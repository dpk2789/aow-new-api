using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using AowCore.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(AowContext repositoryContext)
          : base(repositoryContext)
        {
        }

        public ProductCategory GetProductCategory(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public Task<PagedList<ProductCategory>> GetProductCategories(PagingParameters ownerParameters, Guid cmpId)
        {
            return Task.FromResult(PagedList<ProductCategory>.ToPagedList(FindAll().Where(x => x.CompanyId == cmpId).OrderBy(on => on.Name),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }

    }
}
