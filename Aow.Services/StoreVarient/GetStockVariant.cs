using Aow.Infrastructure.IRepositories;
using System;

namespace Aow.Services.StoreVarient
{
    [Service]
    public class GetStockVariant
    {
        private IRepositoryWrapper _repoWrapper;
        public GetStockVariant(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetStockVariantResponse
        {
            public Guid Id { get; set; }
            public string ItemName { get; set; }
            public decimal? Quantity { get; set; }
            public string UniqueNumber { get; set; }
            public string Status { get; set; }
            public decimal? ConsumedQuantity { get; set; }
            public decimal? ItemAmount { get; set; }
            public decimal? Rate { get; set; }
        }

        public GetStockVariantResponse Do(Guid id)
        {
            var stockVarient = _repoWrapper.StockVarientRepo.GetStockVarient(id);
            var getCompanyResponse = new GetStockVariantResponse
            {
                Id = stockVarient.Id,
                Quantity = stockVarient.Quantity,
                ItemName = stockVarient.ProductVariant.Name,
                UniqueNumber = stockVarient.UniqueNumber,
                Rate = stockVarient.MRPPerUnit,
                ConsumedQuantity = stockVarient.ConsumedQuantity,
                Status = stockVarient.Status,
            };

            return getCompanyResponse;
        }
    }
}
