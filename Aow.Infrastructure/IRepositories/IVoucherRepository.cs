using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IVoucherRepository : IRepositoryBase<Voucher>
    {
        Task<PagedList<Voucher>> GetVouchers(PagingParameters ownerParameters, string voucherName, Guid companyId);
        Voucher GetVoucher(Guid Id);
    }

}
