using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class UserCompanyRepository : RepositoryBase<AppUserCompany>, IUserCompanyRepository
    {
        public UserCompanyRepository(AowContext repositoryContext)
          : base(repositoryContext)
        {
        }

        public Task<PagedList<AppUserCompany>> GetCompaniesByUser(PagingParameters ownerParameters, string userId)
        {
            return Task.FromResult(PagedList<AppUserCompany>.ToPagedList(FindAll().Where(x => x.ApplicationUserId == userId).Include(x => x.Company),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }
}
