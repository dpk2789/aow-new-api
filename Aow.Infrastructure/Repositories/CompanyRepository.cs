using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using System;
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
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public Task<PagedList<Company>> GetCompanies(PagingParameters ownerParameters)
        {
            return Task.FromResult(PagedList<Company>.ToPagedList(FindAll().OrderBy(on => on.Name),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }

      
    }
}
