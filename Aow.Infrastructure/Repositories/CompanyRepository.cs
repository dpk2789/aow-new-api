using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(AowContext repositoryContext)
           : base(repositoryContext)
        {
        }

        public Company GetCompany(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).Include(x => x.UserPayments).FirstOrDefault();
        }

        public async Task<List<Company>> GetAllCompanies()
        {
            var result = await FindAll().
                Include(x => x.FinancialYears).ThenInclude(x => x.Vouchers).
                Include(x => x.ProductCategories).ThenInclude(x => x.Products).
                Include(x => x.LedgerCategories).ThenInclude(x => x.Ledgers).ToListAsync();
            return result;
        }
        public Task<PagedList<Company>> GetCompanies(PagingParameters ownerParameters)
        {
            return Task.FromResult(PagedList<Company>.ToPagedList(FindAll().OrderBy(on => on.Name),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }
}
