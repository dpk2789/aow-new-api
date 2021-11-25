using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class ManufacturingVarientsRepository : RepositoryBase<ManufacturingVarients>, IManufacturingVarientsRepository
    {
        public ManufacturingVarientsRepository(AowContext repositoryContext)
          : base(repositoryContext)
        {
        }

        public ManufacturingVarients GetInputVarient(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public Task<PagedList<ManufacturingVarients>> GetInputVarients(PagingParameters ownerParameters, Guid varientId)
        {
            return Task.FromResult(PagedList<ManufacturingVarients>.ToPagedList(FindAll().Where(x => x.StockProductVariant.ProductVariant.Id == varientId),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }
}
