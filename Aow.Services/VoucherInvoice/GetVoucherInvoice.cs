using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;

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
            public string FinancialYearId { get; set; }
            public string VoucherNumber { get; set; }
            public int VoucherTypeId { get; set; }
            public string VoucherName { get; set; }
            public DateTime Date { get; set; }
            public string RefId { get; set; }
            public string Note { get; set; }
            public decimal Total { get; set; }
            public bool? Type { get; set; }
            public virtual List<GetVoucherInvoiceItemsResponse> VoucherItems { get; set; }
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

        public GetVoucherInvoiceResponse Do(Guid id)
        {
            var voucher = _repoWrapper.VoucherRepo.GetVoucher(id);

            var voucherViewModel = new GetVoucherInvoiceResponse
            {
                Id = voucher.Id,
                Date = voucher.Date,
                VoucherNumber = voucher.VoucherNumber,
                Total = voucher.Total,
                VoucherName = voucher.VoucherName
            };
            var items = new List<GetVoucherInvoiceItemsResponse>();
            if (voucher.VoucherItems != null)
            {
                foreach (var jentry in voucher.VoucherItems)
                {
                    var viewModel = new GetVoucherInvoiceItemsResponse();
                    viewModel.Id = jentry.Id;
                    viewModel.SrNo = jentry.SrNo;
                    viewModel.ItemName = jentry.Product.Name;
                    viewModel.Quantity = jentry.Quantity;
                    viewModel.ItemAmount = jentry.ItemAmount;
                    viewModel.SrNo = jentry.SrNo;
                    viewModel.ProductId = jentry.ProductId;
                    //   viewModel.RootCategory = ledger.Parent.Name;
                    items.Add(viewModel);
                }
                voucherViewModel.VoucherItems = items;
            }
            return voucherViewModel;
        }

    }
}
