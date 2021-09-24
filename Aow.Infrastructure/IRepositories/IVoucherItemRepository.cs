using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IVoucherItemRepository : IRepositoryBase<VoucherItem>
    {
        VoucherItem GetVoucherItem(Guid Id);
        Task<PagedList<VoucherItem>> GetVoucherItems(PagingParameters ownerParameters, string voucherName, Guid companyId);
       
    }
}
