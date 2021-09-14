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
        private IProductCategoryRepository _productCategoryRepository;
        private IProductRepository _productRepository;
        private IProductAttributeRepository _productAttributeRepository;
        private IProductAttributeOptionRepository productAttributeOptionRepository;
        private ILedgerCategoryRepository _ledgerCategoryRepository;
        private ILedgerRepository _ledgerRepository;
        private IJournalEntryRepository journalEntryRepository;
        private IVoucherRepository voucherRepository;
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
        public IVoucherRepository VoucherRepo
        {
            get
            {
                if (voucherRepository == null)
                {
                    voucherRepository = new VoucherRepository(_repoContext);
                }
                return voucherRepository;
            }
        }
        public IJournalEntryRepository JournalEntryRepo
        {
            get
            {
                if (journalEntryRepository == null)
                {
                    journalEntryRepository = new JournalEntryRepository(_repoContext);
                }
                return journalEntryRepository;
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

        public IProductCategoryRepository ProductCategoryRepo
        {
            get
            {
                if (_productCategoryRepository == null)
                {
                    _productCategoryRepository = new ProductCategoryRepository(_repoContext);
                }
                return _productCategoryRepository;
            }
        }
        public IProductRepository ProductRepo
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_repoContext);
                }
                return _productRepository;
            }
        }
        public IProductAttributeRepository ProductAttributeRepo
        {
            get
            {
                if (_productAttributeRepository == null)
                {
                    _productAttributeRepository = new ProductAttributeRepository(_repoContext);
                }
                return _productAttributeRepository;
            }
        }
        public IProductAttributeOptionRepository ProductAttributeOptionRepo
        {
            get
            {
                if (productAttributeOptionRepository == null)
                {
                    productAttributeOptionRepository = new ProductAttributeOptionRepository(_repoContext);
                }
                return productAttributeOptionRepository;
            }
        }

        public ILedgerCategoryRepository LedgerCategoryRepositoryRepo
        {
            get
            {
                if (_ledgerCategoryRepository == null)
                {
                    _ledgerCategoryRepository = new LedgerCategoryRepository(_repoContext);
                }
                return _ledgerCategoryRepository;
            }
        }
        public ILedgerRepository LedgerRepositoryRepo
        {
            get
            {
                if (_ledgerRepository == null)
                {
                    _ledgerRepository = new LedgerRepository(_repoContext);
                }
                return _ledgerRepository;
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
