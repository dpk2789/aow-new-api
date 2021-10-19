using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IUserPaymentRepository : IRepositoryBase<UserPayment>
    {
        UserPayment GetUserPayment(Guid Id);
        Task<PagedList<UserPayment>> GetUserPaymentsByCompany(PagingParameters ownerParameters, string userId, Guid CompanyId);
    }

}
