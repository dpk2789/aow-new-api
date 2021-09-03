using Aow.Infrastructure.IRepositories;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        public RepositoryWrapper(AowContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        private AowContext _repoContext;
        private ICompanyRepository _owner;
        private IFinancialYearRepository _account;
        private IUserCompanyRepository _userCompanyRepository;
        public ICompanyRepository CompanyRepo
        {
            get
            {
                if (_owner == null)
                {
                    _owner = new CompanyRepository(_repoContext);
                }
                return _owner;
            }
        }
        public IFinancialYearRepository FinancialYearRepo
        {
            get
            {
                if (_account == null)
                {
                    _account = new FinancialYearRepository(_repoContext);
                }
                return _account;
            }
        }

        public IUserCompanyRepository UserCompanyRepo
        {
            get
            {
                if (_userCompanyRepository == null)
                {
                    _userCompanyRepository = new UserCompanyRepository(_repoContext);
                }
                return _userCompanyRepository;
            }
        }

        public Task<int> SaveNew()
        {
            return _repoContext.SaveChangesAsync();
        }
        
    }
}
