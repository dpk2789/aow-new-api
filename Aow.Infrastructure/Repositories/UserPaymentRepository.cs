using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using System;
using System.Linq;

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
    }
   
}
