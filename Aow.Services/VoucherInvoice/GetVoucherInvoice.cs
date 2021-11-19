using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Services.VoucherInvoice
{
    [Service]
    public class GetVoucherInvoice
    {
        private IRepositoryWrapper _repoWrapper;
        public GetVoucherInvoice(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetVoucherInvoiceResponse
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
            public virtual List<GetVoucherInvoiceJournalEntries> JournalEntries { get; set; }
            public virtual List<GetVoucherInvoiceItemsResponse> VoucherItems { get; set; }
            public virtual List<GetVoucherInvoiceSundryItems> SundryItems { get; set; }
        }

        public class GetVoucherInvoiceItemsResponse
        {
            public Guid Id { get; set; }
            public int? SrNo { get; set; }
            public decimal? DiscountRatePerUnit { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ItemAmount { get; set; }
            public string ItemName { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public Guid ProductId { get; set; }
        }
        public class GetVoucherInvoiceSundryItems
        {
            public Guid Id { get; set; }
            public int? SrNo { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Percent { get; set; }
            public string Description { get; set; }
            public decimal? ItemAmount { get; set; }
            public Guid LedgerId { get; set; }
            public Guid ProductId { get; set; }
        }
        public class GetVoucherInvoiceJournalEntries
        {
            public Guid? Id { get; set; }
            public Guid? VoucherId { get; set; }
            public int? SrNo { get; set; }
            public string CrDrType { get; set; }
            public Guid LedgerId { get; set; }
            public string InvoiceAccountName { get; set; }
            public string AccountName { get; set; }
            public string RootCategory { get; set; }
            public decimal? CreditAmount { get; set; }
            public decimal? DebitAmount { get; set; }

        }

        public async Task<GetVoucherInvoiceResponse> Do(Guid id)
        {
            var voucher = await _repoWrapper.VoucherRepo.GetVoucher(id);
            decimal ItemsTotal = 0;
            decimal SundryitemsTotal = 0;
            var voucherViewModel = new GetVoucherInvoiceResponse
            {
                Id = voucher.Id,
                Date = voucher.Date,
                VoucherNumber = voucher.VoucherNumber,
                Total = voucher.Total,
                VoucherName = voucher.VoucherName,
                FinancialYearId = voucher.FinancialYearId
            };
            var jEntries = new List<GetVoucherInvoiceJournalEntries>();
            foreach (var jentry in voucher.JournalEntries.OrderBy(x => x.SrNo))
            {
                if (voucher.VoucherName == "Sale Invoice")
                {
                    if (jentry.CrDrType == "Dr")
                    {
                        voucherViewModel.LedgerName = jentry.Ledger.Name;
                        voucherViewModel.LedgerId = jentry.Ledger.Id;
                    }
                }
                if (jentry.SrNo == 1)
                {
                    voucherViewModel.LedgerName = jentry.Ledger.Name;
                    voucherViewModel.LedgerId = jentry.Ledger.Id;
                }
                var jViewModel = new GetVoucherInvoiceJournalEntries();
                jViewModel.Id = jentry.Id;
                jViewModel.CrDrType = jentry.CrDrType;
                jViewModel.AccountName = jentry.Ledger.Name;
                jViewModel.CreditAmount = jentry.CreditAmount;
                jViewModel.DebitAmount = jentry.DebitAmount;
                jViewModel.SrNo = jentry.SrNo;
                jViewModel.VoucherId = jentry.VoucherId;
                // jViewModel.RootCategory = ledger.RootCategory;
                jEntries.Add(jViewModel);
            }
            voucherViewModel.JournalEntries = jEntries;
            var items = new List<GetVoucherInvoiceItemsResponse>();
            if (voucher.VoucherItems != null)
            {
                foreach (var jentry in voucher.VoucherItems)
                {
                    var viewModel = new GetVoucherInvoiceItemsResponse();
                    viewModel.Id = jentry.Id;
                    viewModel.SrNo = jentry.SrNo;
                    viewModel.ItemName = jentry.Product.Name;
                    viewModel.Description = jentry.Description;
                    viewModel.Quantity = jentry.Quantity;
                    viewModel.ItemAmount = jentry.ItemAmount;
                    viewModel.MRPPerUnit = jentry.MRPPerUnit;
                    viewModel.SrNo = jentry.SrNo;
                    viewModel.ProductId = jentry.ProductId;
                    //   viewModel.RootCategory = ledger.Parent.Name;
                    ItemsTotal = jentry.ItemAmount.Value + ItemsTotal;
                    items.Add(viewModel);
                }
                voucherViewModel.VoucherItems = items;
            }
            var sundryItems = new List<GetVoucherInvoiceSundryItems>();
            foreach (var sundryItem in voucher.VoucherSundryItems)
            {
                var sundryItemViewModel = new GetVoucherInvoiceSundryItems();
                sundryItemViewModel.Id = sundryItem.Id;
                //var product = await _productRepository.GetProductById(sundryItem.ProductId);
                sundryItemViewModel.ProductId = sundryItem.Product.Id;
                sundryItemViewModel.LedgerId = sundryItem.Product.Ledger.Id;
                sundryItemViewModel.Name = sundryItem.Product.Name;
                sundryItemViewModel.Percent = sundryItem.Percent;
                sundryItemViewModel.ItemAmount = sundryItem.ItemAmount;
                sundryItemViewModel.SrNo = sundryItem.SrNo;
                SundryitemsTotal = sundryItem.ItemAmount.Value + SundryitemsTotal;
                sundryItems.Add(sundryItemViewModel);
            }
            voucherViewModel.SundryItems = sundryItems;
            voucherViewModel.ItemsTotal = ItemsTotal;
            voucherViewModel.SundryTotal = SundryitemsTotal;
            voucherViewModel.Total = ItemsTotal + SundryitemsTotal;
            return voucherViewModel;
        }

    }
}
