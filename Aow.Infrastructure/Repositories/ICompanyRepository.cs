using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        Company GetCompany(Guid Id);
        Task<PagedList<Company>> GetCompanies(PagingParameters ownerParameters);
    }
}
