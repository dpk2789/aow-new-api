using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IVoucherRepository : IRepositoryBase<Voucher>
    {
        Task<Voucher> GetVoucherForStock(Guid Id);
        Task<PagedList<Voucher>> GetVouchers(PagingParameters ownerParameters, string voucherName, Guid companyId);
        Task<Voucher> GetVoucherVairents(Guid Id);
        Task<Voucher> GetVoucher(Guid Id);
        Task<Voucher> GetVoucherForDelete(Guid Id);
    }

}
