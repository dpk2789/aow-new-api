using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class VoucherRepository : RepositoryBase<Voucher>, IVoucherRepository
    {
        public VoucherRepository(AowContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<Voucher> GetVoucher(Guid Id)
        {
            var result = await FindByCondition(x => x.Id == Id).Include(x => x.JournalEntries).ThenInclude(x => x.Ledger).
            Include(x => x.VoucherSundryItems).ThenInclude(x => x.Product).ThenInclude(x => x.Ledger).
            Include(x => x.VoucherItems).ThenInclude(x => x.Product).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Voucher> GetVoucherForDelete(Guid Id)
        {
            var result = await FindByCondition(x => x.Id == Id).Include(x => x.JournalEntries).Include(x => x.VoucherSundryItems).
            Include(x => x.VoucherItems).FirstOrDefaultAsync();
            return result;
        }

        public Task<PagedList<Voucher>> GetVouchers(PagingParameters ownerParameters, string voucherName, Guid FinancialYearId)
        {
            return Task.FromResult(PagedList<Voucher>.ToPagedList(FindAll().Where(x => x.FinancialYearId == FinancialYearId && x.VoucherName == voucherName).
                Include(x => x.JournalEntries).Include(x => x.VoucherItems).OrderBy(on => on.VoucherName),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }

}
