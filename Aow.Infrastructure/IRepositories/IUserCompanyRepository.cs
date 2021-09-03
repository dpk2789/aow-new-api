using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public interface IUserCompanyRepository : IRepositoryBase<AppUserCompany>
    {
        Task<PagedList<AppUserCompany>> GetCompaniesByUser(PagingParameters ownerParameters, string userId);
    }
}
