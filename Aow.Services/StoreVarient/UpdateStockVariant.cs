using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.StoreVarient
{
    [Service]
    public class UpdateStockVariant
    {
        private IRepositoryWrapper _repoWrapper;
        public UpdateStockVariant(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class UpdateStockVariantRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int? SrNo { get; set; }
            public string UniqueNumber { get; set; }
            public string Status { get; set; }
            public string ItemName { get; set; }
            public string Description { get; set; }
            public decimal? SalePrice { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ConsumedQuantity { get; set; }
            public decimal? ItemAmount { get; set; }
            public decimal Rate { get; set; }
        }
        public class UpdateStockVariantResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }
        public async Task<UpdateStockVariantResponse> Do(UpdateStockVariantRequest request)
        {
            try
            {
                var stockVariant = _repoWrapper.StockVarientRepo.GetStockVarient(request.Id);
                if (stockVariant == null)
                {
                    return null;
                }

                stockVariant.ConsumedQuantity = request.ConsumedQuantity;
                _repoWrapper.StockVarientRepo.Update(stockVariant);

                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new UpdateStockVariantResponse
                    {
                        Name = request.Name,
                        Success = false,
                        Description = "Some Error Occur"
                    };
                }
                else
                {
                    return new UpdateStockVariantResponse
                    {
                        Id = stockVariant.Id,
                        Name = request.Name,
                        Success = true,
                        Description = "Successfully Updated Stock Variant!!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new UpdateStockVariantResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
