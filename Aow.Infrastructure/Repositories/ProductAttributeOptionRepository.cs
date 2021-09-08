using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class ProductAttributeOptionRepository : RepositoryBase<ProductAttributeOptions>, IProductAttributeOptionRepository
    {
        public ProductAttributeOptionRepository(AowContext repositoryContext)
         : base(repositoryContext)
        {
        }

        public ProductAttributeOptions GetProductAttributeOption(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public Task<PagedList<ProductAttributeOptions>> GetProductAttributeOptions(PagingParameters ownerParameters, Guid cmpId)
        {
            return Task.FromResult(PagedList<ProductAttributeOptions>.ToPagedList(FindAll().Where(x => x.ProductAttributeId == cmpId).OrderBy(on => on.Name),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }
}
