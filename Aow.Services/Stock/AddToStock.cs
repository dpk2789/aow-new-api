using Aow.Infrastructure.IRepositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Services.Stock
{
    [Service]
    public class AddToStock
    {
        private IRepositoryWrapper _repoWrapper;
        public AddToStock(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class AddToStockRequest
        {
            public Guid VoucherId { get; set; }
        }
        public class AddToStockResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }
        public async Task<AddToStockResponse> Do(AddToStockRequest request)
        {
            try
            {
                var updateVoucher = await _repoWrapper.VoucherRepo.GetVoucherForStock(request.VoucherId);
                if (updateVoucher != null)
                {
                    if (updateVoucher.VoucherItems != null)
                    {
                        var voucherItems = updateVoucher.VoucherItems.ToList();
                        if (voucherItems.Count != 0)
                        {
                            foreach (var item in voucherItems)
                            {
                                var retriveStock = await _repoWrapper.StockRepo.GetStockByVoucherItemId(item.Id);
                                if (retriveStock != null)
                                {
                                    foreach (var stock in retriveStock)
                                    {
                                        _repoWrapper.StockRepo.Delete(stock);
                                    }
                                }
                                Guid stockId = Guid.NewGuid();
                                var stockNew = new Aow.Infrastructure.Domain.Stock
                                {
                                    Id = stockId,
                                    MRPPerUnit = item.MRPPerUnit,
                                    Price = item.MRPPerUnit.Value,
                                    Quantity = item.Quantity,
                                    ProductId = item.ProductId,
                                    ItemAmount = item.ItemAmount,
                                    VoucherItemId = item.Id
                                };
                                _repoWrapper.StockRepo.Create(stockNew);
                                foreach (var variant in item.VoucherItemVariants)
                                {
                                    var variantNew = new Aow.Infrastructure.Domain.StockProductVariant
                                    {
                                        Id = Guid.NewGuid(),
                                        MRPPerUnit = variant.MRPPerUnit,
                                        Price = variant.MRPPerUnit.Value,
                                        Quantity = variant.UnitQuantity,
                                        ProductVariantId = variant.ProductVariantId,
                                        ItemAmount = variant.ItemAmount,
                                        StockId = stockId
                                    };
                                    _repoWrapper.StockVarientRepo.Create(variantNew);
                                }
                            }
                        }
                    }
                }
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new AddToStockResponse
                    {
                        Description = "Not Added SuccessFully",
                        Success = false
                    };
                }
                else
                {
                    return new AddToStockResponse
                    {
                        Success = true,
                        Description = "Product Category SuccessFully Added"
                    };
                }
            }
            catch (Exception ex)
            {
                return new AddToStockResponse
                {
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
