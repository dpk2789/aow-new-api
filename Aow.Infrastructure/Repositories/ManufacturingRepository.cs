using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class ManufacturingRepository : RepositoryBase<Manufacture>, IManufacturingRepository
    {
        public ManufacturingRepository(AowContext repositoryContext)
          : base(repositoryContext)
        {
        }

        public Manufacture GetManufacture(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public Task<PagedList<Manufacture>> GetManufactures(PagingParameters ownerParameters, Guid financialYearId)
        {
            return Task.FromResult(PagedList<Manufacture>.ToPagedList(FindAll().Where(x => x.FinancialYearId == financialYearId).OrderBy(on => on.Date).
                Include(x => x.ManufactureItems).ThenInclude(x => x.Stock.Product), ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }

}
