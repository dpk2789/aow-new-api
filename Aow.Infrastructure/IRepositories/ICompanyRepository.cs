using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        Company GetCompany(Guid Id);
        Task<List<Company>> GetAllCompanies();
        Task<PagedList<Company>> GetCompanies(PagingParameters ownerParameters);
    }
}
