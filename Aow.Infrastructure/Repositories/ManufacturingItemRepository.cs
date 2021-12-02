using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class ManufacturingItemRepository : RepositoryBase<ManufactureItem>, IManufacturingItemRepository
    {
        public ManufacturingItemRepository(AowContext repositoryContext)
          : base(repositoryContext)
        {
        }

        public ManufactureItem GetManufacturingItem(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public Task<PagedList<ManufactureItem>> GetManufacturingItems(PagingParameters ownerParameters, Guid varientId)
        {
            return Task.FromResult(PagedList<ManufactureItem>.ToPagedList(FindAll().Where(x => x.Stock.Product.Id == varientId),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }
}
