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

        public Voucher GetVoucher(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).Include(x => x.JournalEntries).FirstOrDefault();
        }

        public Task<PagedList<Voucher>> GetVouchers(PagingParameters ownerParameters, string voucherName, Guid FinancialYearId)
        {
            return Task.FromResult(PagedList<Voucher>.ToPagedList(FindAll().Where(x => x.FinancialYearId == FinancialYearId && x.VoucherName == voucherName).
                
                Include(x => x.JournalEntries).OrderBy(on => on.VoucherName),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }

}
