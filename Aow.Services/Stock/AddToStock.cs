using Aow.Infrastructure.IRepositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Services.Store
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
            public string Name { get; set; }
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
                var updateVoucher = await _repoWrapper.VoucherRepo.GetVoucherForDelete(request.VoucherId);
                if (updateVoucher != null)
                {
                    if (updateVoucher.VoucherItems != null)
                    {
                        var voucherItems = updateVoucher.VoucherItems.ToList();
                        if (voucherItems.Count != 0)
                        {
                            foreach (var item in voucherItems)
                            {
                                var stock = new Aow.Infrastructure.Domain.Stock
                                {
                                    Id = Guid.NewGuid(),
                                    MRPPerUnit = item.MRPPerUnit,
                                    Price = item.MRPPerUnit.Value,
                                    Quantity = item.Quantity,
                                    ProductId = item.ProductId,
                                    ItemAmount = item.ItemAmount,
                                    VoucherItemId = item.Id
                                };
                                _repoWrapper.StockRepo.Create(stock);

                            }
                        }
                    }
                }
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new AddToStockResponse
                    {
                        Name = request.Name,
                        Success = false
                    };
                }
                else
                {
                    return new AddToStockResponse
                    {                     
                        Name = request.Name,
                        Success = true,
                        Description = "Product Category SuccessFully Added"
                    };
                }
            }
            catch (Exception ex)
            {
                return new AddToStockResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
