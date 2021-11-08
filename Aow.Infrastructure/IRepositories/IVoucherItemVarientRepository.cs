using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IVoucherItemVarientRepository : IRepositoryBase<VoucherItemVariant>
    {
        VoucherItemVariant GetVoucherItemVarient(Guid Id);
        Task<PagedList<VoucherItemVariant>> GetVoucherItemVarients(PagingParameters ownerParameters, Guid voucherItemId);
    }
}
