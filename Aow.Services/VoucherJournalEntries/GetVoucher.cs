using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;

namespace Aow.Services.Voucher
{
    [Service]
    public class GetVoucher
    {
        private IRepositoryWrapper _repoWrapper;
        public GetVoucher(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetVoucherResponse
        {
            public Guid Id { get; set; }
            public string FinancialYearId { get; set; }
            public string VoucherNumber { get; set; }
            public int VoucherTypeId { get; set; }
            public string VoucherName { get; set; }
            public DateTime Date { get; set; }
            public string RefId { get; set; }
            public string Note { get; set; }
            public decimal Total { get; set; }
            public bool? Type { get; set; }
            public virtual List<GetVoucherJournalEntriesResponse> JournalEntries { get; set; }
        }

        public class GetVoucherJournalEntriesResponse
        {
            public Guid? Id { get; set; }
            public Guid? VoucherId { get; set; }
            public string VoucherName { get; set; }
            public string VoucherNumber { get; set; }
            public DateTime VoucherDate { get; set; }
            public decimal? VoucherTotal { get; set; }
            public int? SrNo { get; set; }
            public string CrDrType { get; set; }
            public Guid LedgerId { get; set; }
            public string InvoiceAccountName { get; set; }
            public string AccountName { get; set; }
            public string RootCategory { get; set; }
            public decimal? CreditAmount { get; set; }
            public decimal? DebitAmount { get; set; }
            public string RefId { get; set; }       
            public decimal? Quantity { get; set; }
            public decimal? ItemAmount { get; set; }          
        }

        public GetVoucherResponse Do(Guid id)
        {
            var voucher = _repoWrapper.VoucherRepo.GetVoucher(id);
       
            var voucherViewModel = new GetVoucherResponse
            {
                Id = voucher.Id,
                Date = voucher.Date,
                VoucherNumber = voucher.VoucherNumber,
                Total = voucher.Total,
                VoucherName = voucher.VoucherName
            };
            var items = new List<GetVoucherJournalEntriesResponse>();
            foreach (var jentry in voucher.JournalEntries)
            {
                var ledger = _repoWrapper.LedgerRepositoryRepo.GetLedger(jentry.LedgerId);              
                var viewModel = new GetVoucherJournalEntriesResponse();
                viewModel.Id = jentry.Id;
                viewModel.CrDrType = jentry.CrDrType;
                viewModel.VoucherDate = jentry.Date;
                viewModel.AccountName = ledger.Name;
                viewModel.VoucherNumber = jentry.Vouchers.VoucherNumber;
                viewModel.CreditAmount = jentry.CreditAmount;
                viewModel.DebitAmount = jentry.DebitAmount;
                viewModel.SrNo = jentry.SrNo;
                viewModel.VoucherId = jentry.VoucherId;
                //   viewModel.RootCategory = ledger.Parent.Name;
                items.Add(viewModel);
            }
            voucherViewModel.JournalEntries = items;           

            return voucherViewModel;
        }
    }
}
