using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly UserManager<AppUser> _userManager;

        public RepositoryWrapper(AowContext repositoryContext, UserManager<AppUser> userManager)
        {
            _repoContext = repositoryContext;
            _userManager = userManager;
        }
        private AowContext _repoContext;
        private ICompanyRepository _owner;
        private IFinancialYearRepository _account;
        private IUserCompanyRepository _userCompanyRepository;
        private IUserRepository _userRepository;

        public IUserRepository UserRepo
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_userManager);
                }
                return _userRepository;
            }
        }

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

        public async Task<int> SaveNew()
        {
            var result = await _repoContext.SaveChangesAsync();
            return result;
        }

        public int Save()
        {
            var result = _repoContext.SaveChanges();
            return result;
        }

    }
}
