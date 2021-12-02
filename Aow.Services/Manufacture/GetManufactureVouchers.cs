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
            public List<ManufacturingVarientsResponse> ManufacturingVarients { get; set; }
        }
        public class ManufacturingVarientsResponse
        {
            public Guid Id { get; set; }
            public string VairentName { get; set; }
            public decimal? Quantity { get; set; }
            public string Type { get; set; }
            public Guid? StockProductVariantId { get; set; }
            public Guid ManufactureId { get; set; }
        }
        public IEnumerable<GetManufactureVouchersResponse> Do(PagingParameters pagingParameters, Guid fyrId)
        {
            var fyr = _repoWrapper.FinancialYearRepo.GetFinancialYear(fyrId);
            if (fyr == null)
            {
                return null;
            };
            var list = _repoWrapper.ManufacturingRepo.GetManufactures(pagingParameters, fyrId).GetAwaiter().GetResult();
            var manufactureVoucherList = new List<GetManufactureVouchersResponse>();
            foreach (var voucher in list)
            {
                var manufactureVoucher = new GetManufactureVouchersResponse();
                manufactureVoucher.Id = voucher.Id;
                manufactureVoucher.VoucherNumber = voucher.VoucherNumber;
                manufactureVoucher.Date = voucher.Date;
                manufactureVoucherList.Add(manufactureVoucher);
                var manufacturingVarientsResponseList = new List<ManufacturingVarientsResponse>();
                foreach (var item in voucher.ManufactureItems)
                {
                    var manufacturingVarient = new ManufacturingVarientsResponse();
                    manufacturingVarient.Id = item.Id;
                    manufacturingVarient.VairentName = item.Stock.Product.Name;
                    manufacturingVarient.Quantity = item.Quantity;
                    manufacturingVarientsResponseList.Add(manufacturingVarient);
                }
                manufactureVoucher.ManufacturingVarients = manufacturingVarientsResponseList;
            }
          
            return manufactureVoucherList;
        }
    }
}
