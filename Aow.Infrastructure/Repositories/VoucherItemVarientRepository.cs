using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class VoucherItemVarientRepository : RepositoryBase<VoucherItemVariant>, IVoucherItemVarientRepository
    {
        public VoucherItemVarientRepository(AowContext repositoryContext)
         : base(repositoryContext)
        {
        }

        public VoucherItemVariant GetVoucherItemVarient(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public Task<PagedList<VoucherItemVariant>> GetVoucherItemVarients(PagingParameters ownerParameters, Guid voucherItemId)
        {
            return Task.FromResult(PagedList<VoucherItemVariant>.ToPagedList(FindAll().Where(x => x.VoucherItem.Id == voucherItemId),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }
}
