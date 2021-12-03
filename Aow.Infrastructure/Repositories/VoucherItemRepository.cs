using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class VoucherItemRepository : RepositoryBase<VoucherItem>, IVoucherItemRepository
    {
        public VoucherItemRepository(AowContext repositoryContext)
         : base(repositoryContext)
        {
        }

        public async Task<VoucherItem> GetVoucherItem(Guid Id)
        {
            var result = await FindByCondition(x => x.Id == Id).Include(x => x.Product).
                Include(x => x.VoucherItemVariants).Include(x => x.Voucher.JournalEntries).ThenInclude(x => x.Ledger).FirstOrDefaultAsync();
            return result;
        }

        public Task<PagedList<VoucherItem>> GetVoucherItems(PagingParameters ownerParameters, string voucherName, Guid FinancialYearId)
        {
            return Task.FromResult(PagedList<VoucherItem>.ToPagedList(FindAll().Where(x => x.Voucher.FinancialYearId == FinancialYearId),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }
}
