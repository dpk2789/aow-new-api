using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aow.Services.VoucherInvoice
{
    [Service]
    public class GetAllVoucherVarients
    {
        private IRepositoryWrapper _repoWrapper;
        public GetAllVoucherVarients(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetAllVoucherVarientsResponse
        {
            public Guid Id { get; set; }
            public Guid? FinancialYearId { get; set; }
            public string VoucherNumber { get; set; }
            public int VoucherTypeId { get; set; }
            public string VoucherName { get; set; }
            public DateTime Date { get; set; }
            public Guid LedgerId { get; set; }
            public string LedgerName { get; set; }
            public string RefId { get; set; }
            public string Note { get; set; }
            public decimal Total { get; set; }
            public decimal ItemsTotal { get; set; }
            public decimal SundryTotal { get; set; }
            public bool? Type { get; set; }
        }

        public async Task<IEnumerable<GetAllVoucherVarientsResponse>> Do(Guid id)
        {
            var voucher = await _repoWrapper.VoucherRepo.GetVoucher(id);
            var varientList = new List<GetAllVoucherVarientsResponse>();

            foreach (var item in voucher.VoucherItems)
            {
                foreach (var varient in item.VoucherItemVariants)
                {
                    GetAllVoucherVarientsResponse getAllVoucherVarientsResponse = new GetAllVoucherVarientsResponse();
                    getAllVoucherVarientsResponse.Id = varient.Id;

                    varientList.Add(getAllVoucherVarientsResponse);

                }
            }

            return varientList;
        }
    }
}
