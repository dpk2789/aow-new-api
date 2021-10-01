using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public  interface IVoucherSundryItemRepository : IRepositoryBase<VoucherSundryItem>
    {
        VoucherSundryItem GetVoucherSundryItem(Guid Id);
        Task<PagedList<VoucherSundryItem>> GetVoucherSundryItems(PagingParameters ownerParameters, string voucherName, Guid companyId);
    }
}
