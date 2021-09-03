using Aow.Infrastructure.Domain;

namespace Aow.Infrastructure.Repositories
{
    public class UserCompanyRepository : RepositoryBase<AppUserCompany>, IUserCompanyRepository
    {
        public UserCompanyRepository(AowContext repositoryContext)
          : base(repositoryContext)
        {
        }
    }
}
