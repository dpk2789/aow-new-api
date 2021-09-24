﻿using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
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

        public VoucherItem GetVoucherItem(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public Task<PagedList<VoucherItem>> GetVoucherItems(PagingParameters ownerParameters, string voucherName, Guid FinancialYearId)
        {
            return Task.FromResult(PagedList<VoucherItem>.ToPagedList(FindAll().Where(x => x.Voucher.FinancialYearId == FinancialYearId),
                                    ownerParameters.PageNumber, ownerParameters.PageSize));
        }
    }
}
