using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class VoucherSundryItemRepository : RepositoryBase<VoucherSundryItem>, IVoucherSundryItemRepository
    {
        public VoucherSundryItemRepository(AowContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public VoucherSundryItem GetVoucherSundryItem(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public Task<PagedList<VoucherSundryItem>> GetVoucherSundryItems(PagingParameters ownerParameters, string voucherName, Guid FinancialYearId)
        {
            return Task.FromResult(PagedList<VoucherSundryItem>.ToPagedList(FindAll().Where(x => x.Voucher.FinancialYearId == FinancialYearId),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }
}
