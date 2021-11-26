using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.Manufacture
{
    [Service]
    public class GetManufactureVouchers
    {
        private IRepositoryWrapper _repoWrapper;
        public GetManufactureVouchers(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetManufactureVouchersResponse
        {
            public Guid Id { get; set; }
            public DateTime? Date { get; set; }
            public string VoucherNumber { get; set; }
            public string VoucherName { get; set; }
            public string RefId { get; set; }
            public string Note { get; set; }
            public bool? Type { get; set; }
            public decimal? Total { get; set; }

        }

        public IEnumerable<GetManufactureVouchersResponse> Do(PagingParameters pagingParameters, Guid companyId)
        {
            var user = _repoWrapper.CompanyRepo.GetCompany(companyId);
            if (user == null)
            {
                return null;
            };
            var list = _repoWrapper.ManufacturingRepo.GetManufactures(pagingParameters, companyId).GetAwaiter().GetResult();
            var newList = list.Select(x => new GetManufactureVouchersResponse
            {
                Id = x.Id,
                VoucherNumber = x.VoucherNumber,
            });

            return newList;
        }
    }
}
