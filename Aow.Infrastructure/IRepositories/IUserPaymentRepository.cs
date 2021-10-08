using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Repositories;
using System;

namespace Aow.Infrastructure.IRepositories
{
    public interface IUserPaymentRepository : IRepositoryBase<UserPayment>
    {
        UserPayment GetUserPayment(Guid Id);       
    }

}
