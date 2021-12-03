using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;

namespace Aow.Services.Stock
{
    [Service]
    public class GetCurrentStock
    {
        private IRepositoryWrapper _repoWrapper;
        public GetCurrentStock(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetCurrentStockResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public string VoucherName { get; set; }
            public string VoucherNumber { get; set; }
            public string Type { get; set; }
            public string LedgerName { get; set; }
            public decimal? SalePrice { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ItemAmount { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public Guid? VoucherItemId { get; set; }
            public List<GetStockProductVariantResponse> StockProductVariants { get; set; }
        }

        public class GetStockProductVariantResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string ModelNumber { get; set; }
            public string Size { get; set; }
            public decimal? SalePrice { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ConsumedQuantity { get; set; }
            public string Status { get; set; }
            public decimal? ItemAmount { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public Guid? ProductVariantId { get; set; }
            public string ProductVariantName { get; set; }
        }

        public IEnumerable<GetCurrentStockResponse> Do(PagingParameters pagingParameters, Guid companyId)
        {
            var list = _repoWrapper.StockRepo.GetStocks(pagingParameters, companyId).GetAwaiter().GetResult();
            var stockList = new List<GetCurrentStockResponse>();
            foreach (var item in list)
            {
                if (item.VoucherItemId != null)
                {
                    var voucherItem = _repoWrapper.VoucherItemRepo.GetVoucherItem(item.VoucherItemId.Value).GetAwaiter().GetResult();

                    if (voucherItem != null)
                    {
                        var varientList = new List<GetStockProductVariantResponse>();
                        GetCurrentStockResponse newStock = new GetCurrentStockResponse
                        {
                            Id = item.Id,
                            Quantity = item.Quantity,
                            VoucherName = voucherItem.Voucher.VoucherName,
                            Name = item.Product.Name,
                            Date = voucherItem.Voucher.Date,
                            VoucherNumber = voucherItem.Voucher.VoucherNumber
                        };
                        foreach (var jentry in voucherItem.Voucher.JournalEntries)
                        {
                            if (jentry.SrNo == 1)
                            {
                                newStock.LedgerName = jentry.Ledger.Name;
                            }
                        }
                        stockList.Add(newStock);
                        foreach (var varient in item.StockProductVariants)
                        {
                            var getStockProductVariantResponse = new GetStockProductVariantResponse
                            {
                                Id = varient.Id,
                                Name = varient.ProductVariant.Name,
                                Quantity = varient.Quantity
                            };
                            varientList.Add(getStockProductVariantResponse);
                        }
                        newStock.StockProductVariants = varientList;
                    }
                }
                else
                {
                    var varientList = new List<GetStockProductVariantResponse>();
                    GetCurrentStockResponse newStock = new GetCurrentStockResponse
                    {
                        Id = item.Id,
                        Quantity = item.Quantity,
                        Name = item.Product.Name,
                    };
                    stockList.Add(newStock);
                    foreach (var varient in item.StockProductVariants)
                    {
                        var getStockProductVariantResponse = new GetStockProductVariantResponse
                        {
                            Id = varient.Id,
                            Name = varient.ProductVariant.Name,
                            Quantity = varient.Quantity
                        };
                        varientList.Add(getStockProductVariantResponse);
                    }
                    newStock.StockProductVariants = varientList;
                }
            }
            return stockList;
        }
    }
}
