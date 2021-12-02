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
        private IUserPaymentRepository _userPaymentRepository;
        private IProductCategoryRepository _productCategoryRepository;
        private IProductRepository _productRepository;
        private IProductAttributeRepository _productAttributeRepository;
        private IProductAttributeOptionRepository productAttributeOptionRepository;
        private ILedgerCategoryRepository _ledgerCategoryRepository;
        private ILedgerRepository _ledgerRepository;
        private IJournalEntryRepository journalEntryRepository;
        private IVoucherRepository voucherRepository;
        private IVoucherItemRepository voucherItemRepository;
        private IVoucherSundryItemRepository _voucherSundryItemRepository;
        private IProductVarientRepository _productVarientRepository;
        private IProductVariantAndOptionRepository _productVariantAndOptionRepository;
        private IVoucherItemVarientRepository _voucherItemVarientRepository;
        private IStockRepository _stockRepository;
        private IStockVarientRepository _stockVarientRepository;
        private IManufacturingRepository _manufacturingRepo;
        private IManufacturingItemRepository _manufacturingItemRepository;
        private IManufacturingVarientsRepository _manufacturingVarientsRepository;

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
        public IProductVarientRepository ProductVarientRepo
        {
            get
            {
                if (_productVarientRepository == null)
                {
                    _productVarientRepository = new ProductVarientRepository(_repoContext);
                }
                return _productVarientRepository;
            }
        }
        public IStockRepository StockRepo
        {
            get
            {
                if (_stockRepository == null)
                {
                    _stockRepository = new StockRepository(_repoContext);
                }
                return _stockRepository;
            }
        }
        public IStockVarientRepository StockVarientRepo
        {
            get
            {
                if (_stockVarientRepository == null)
                {
                    _stockVarientRepository = new StockVarientRepository(_repoContext);
                }
                return _stockVarientRepository;
            }
        }
        public IManufacturingRepository ManufacturingRepo
        {
            get
            {
                if (_manufacturingRepo == null)
                {
                    _manufacturingRepo = new ManufacturingRepository(_repoContext);
                }
                return _manufacturingRepo;
            }
        }
        public IManufacturingItemRepository ManufacturingItemRepo
        {
            get
            {
                if (_manufacturingItemRepository == null)
                {
                    _manufacturingItemRepository = new ManufacturingItemRepository(_repoContext);
                }
                return _manufacturingItemRepository;
            }
        }
        public IManufacturingVarientsRepository ManufactureVarientsRepo
        {
            get
            {
                if (_manufacturingVarientsRepository == null)
                {
                    _manufacturingVarientsRepository = new ManufacturingVarientsRepository(_repoContext);
                }
                return _manufacturingVarientsRepository;
            }
        }

        public IVoucherItemVarientRepository VoucherItemVarientRepo
        {
            get
            {
                if (_voucherItemVarientRepository == null)
                {
                    _voucherItemVarientRepository = new VoucherItemVarientRepository(_repoContext);
                }
                return _voucherItemVarientRepository;
            }
        }
        public IProductVariantAndOptionRepository ProductVariantAndOptionRepo
        {
            get
            {
                if (_productVariantAndOptionRepository == null)
                {
                    _productVariantAndOptionRepository = new ProductVariantAndOptionRepository(_repoContext);
                }
                return _productVariantAndOptionRepository;
            }
        }
        public IUserPaymentRepository UserPaymentRepo
        {
            get
            {
                if (_userPaymentRepository == null)
                {
                    _userPaymentRepository = new UserPaymentRepository(_repoContext);
                }
                return _userPaymentRepository;
            }
        }
        public IVoucherSundryItemRepository VoucherSundryItemRepo
        {
            get
            {
                if (_voucherSundryItemRepository == null)
                {
                    _voucherSundryItemRepository = new VoucherSundryItemRepository(_repoContext);
                }
                return _voucherSundryItemRepository;
            }
        }
        public IVoucherItemRepository VoucherItemRepo
        {
            get
            {
                if (voucherItemRepository == null)
                {
                    voucherItemRepository = new VoucherItemRepository(_repoContext);
                }
                return voucherItemRepository;
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
