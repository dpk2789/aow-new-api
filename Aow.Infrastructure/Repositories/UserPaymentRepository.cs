using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class UserPaymentRepository : RepositoryBase<UserPayment>, IUserPaymentRepository
    {
        public UserPaymentRepository(AowContext repositoryContext)
          : base(repositoryContext)
        {
        }

        public UserPayment GetUserPayment(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public Task<PagedList<UserPayment>> GetUserPaymentsByCompany(PagingParameters ownerParameters, string userId, Guid CompanyId)
        {
            return Task.FromResult(PagedList<UserPayment>.ToPagedList(FindAll().Where(x => x.CompanyId == CompanyId && x.AppUserId == userId).OrderBy(on => on.CreatedUtc),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }

}
