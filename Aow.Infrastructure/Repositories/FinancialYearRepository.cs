using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class FinancialYearRepository : RepositoryBase<FinancialYear>, IFinancialYearRepository
    {
        public FinancialYearRepository(AowContext repositoryContext)
          : base(repositoryContext)
        {
        }

        public FinancialYear GetFinancialYear(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public Task<PagedList<FinancialYear>> GetFinancialYears(PagingParameters ownerParameters)
        {
            return Task.FromResult(PagedList<FinancialYear>.ToPagedList(FindAll().OrderBy(on => on.Name),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }
}
