using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Services.Voucher
{
    [Service]
    public class GetVouchers
    {
        private IRepositoryWrapper _repoWrapper;
        public GetVouchers(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetVouchersResponse
        {
            public Guid Id { get; set; }
            public DateTime? Date { get; set; }
            public string VoucherNumber { get; set; }
            public string VoucherName { get; set; }
            public string RefId { get; set; }
            public string Note { get; set; }
            public bool? Type { get; set; }
            public decimal? Total { get; set; }
            public virtual IEnumerable<JournalEntriesResponse> JournalEntries { get; set; }
        }
        public class JournalEntriesResponse
        {
            public Guid Id { get; set; }
            public Guid? VoucherId { get; set; }
            public string AccountName { get; set; }
            public string VoucherName { get; set; }
            public string VoucherInvoice { get; set; }
            public DateTime VoucherDate { get; set; }
            public decimal? VoucherTotal { get; set; }
            public int? SrNo { get; set; }
            public bool? OnRecord { get; set; }
            public Guid? RefAccountId { get; set; }
            public string AccountType { get; set; }
            public string CrDrType { get; set; }
            public decimal? CreditAmount { get; set; }
            public decimal? DebitAmount { get; set; }
        }
        public IEnumerable<GetVouchersResponse> Do(PagingParameters pagingParameters, Guid cmpidG, Guid FinancialYearId)
        {
            var financialYear = _repoWrapper.FinancialYearRepo.GetFinancialYear(FinancialYearId);
            if (financialYear == null)
            {
                return null;
            };
            var list = _repoWrapper.VoucherRepo.GetVouchers(pagingParameters, financialYear.Id).GetAwaiter().GetResult();
            var ledgers = _repoWrapper.LedgerRepositoryRepo.GetLedgers(pagingParameters, cmpidG);
            var result = ledgers.GetAwaiter().GetResult();

            var voucherList = new List<GetVouchersResponse>();

            foreach (var voucher in list)
            {
                var voucherViewModel = new GetVouchersResponse
                {
                    Id = voucher.Id,
                    Date = voucher.Date,
                    VoucherNumber = voucher.VoucherNumber,
                    Total = voucher.Total,
                    VoucherName = voucher.VoucherName
                };
                var items = new List<JournalEntriesResponse>();
                foreach (var jentry in voucher.JournalEntries)
                {
                    var ledger = result.FirstOrDefault(x => x.Id == jentry.LedgerId);
                    var viewModel = new JournalEntriesResponse();
                    viewModel.Id = jentry.Id;
                    viewModel.CrDrType = jentry.CrDrType;
                    viewModel.VoucherDate = jentry.Date;
                    viewModel.AccountName = ledger.Name;
                    viewModel.VoucherInvoice = jentry.Vouchers.VoucherNumber;
                    viewModel.CreditAmount = jentry.CreditAmount;
                    viewModel.DebitAmount = jentry.DebitAmount;
                    viewModel.SrNo = jentry.SrNo;
                    viewModel.VoucherId = jentry.VoucherId;
                    //   viewModel.RootCategory = ledger.Parent.Name;
                    items.Add(viewModel);
                }
                voucherViewModel.JournalEntries = items;
                voucherList.Add(voucherViewModel);
            }

            return voucherList;
        }
    }
}






//var newList = list.Select(x => new GetVouchersResponse
//{
//    Id = x.Id,
//    //VoucherName = x.VoucherName,
//    //Date = x.Date,
//    //VoucherTypeId = x.VoucherTypeId,
//    //Total = x.Total,

//    JournalEntries = x.JournalEntries.Select(y => new JournalEntriesResponse
//    {
//        Id = y.Id,
//        //VoucherDate = y.Vouchers.Date,
//        //CrDrType = y.CrDrType,
//        //SrNo = y.SrNo,
//        //VoucherId = y.VoucherId,
//        //CreditAmount = y.CreditAmount,
//        //DebitAmount = y.DebitAmount,
//    }),

//});