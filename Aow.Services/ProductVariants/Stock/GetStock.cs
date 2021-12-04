using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;

namespace Aow.Services.Stock
{
    [Service]
    public class GetStock
    {
        private IRepositoryWrapper _repoWrapper;
        public GetStock(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetStockResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ItemAmount { get; set; }
            public decimal Price { get; set; }
            public Guid ProductId { get; set; }
            public List<UpdateStockVariantResponse> Varients { get; set; }
        }
        public class UpdateStockVariantResponse
        {
            public Guid Id { get; set; }
            public Guid? VoucherItemVarientId { get; set; }
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
            public decimal Price { get; set; }
            public Guid? ProductVariantId { get; set; }
            public Guid StockId { get; set; }
        }
        public GetStockResponse Do(Guid Id)
        {
            var stock = _repoWrapper.StockRepo.GetStock(Id);
            if (stock == null)
            {
                return null;
            }
            var vareintList = new List<UpdateStockVariantResponse>();
            var getProductViewModel = new GetStockResponse();
            getProductViewModel.Id = stock.Id;
            getProductViewModel.Quantity = stock.Quantity;
            getProductViewModel.ProductId = stock.ProductId.Value;
            getProductViewModel.Name = stock.Product.Name;
            foreach (var variant in stock.StockProductVariants)
            {
                var getProductVarientsResponse = new UpdateStockVariantResponse();
                getProductVarientsResponse.Id = variant.Id;
                getProductVarientsResponse.ItemName = variant.ProductVariant.Name;
                getProductVarientsResponse.Quantity = variant.Quantity;
                getProductVarientsResponse.ConsumedQuantity = variant.ConsumedQuantity;
                getProductVarientsResponse.Status = variant.Status;
                getProductVarientsResponse.ItemAmount = variant.ItemAmount;
                getProductVarientsResponse.MRPPerUnit = variant.MRPPerUnit;
                vareintList.Add(getProductVarientsResponse);
            }
            getProductViewModel.Varients = vareintList;
            return getProductViewModel;
          
        }
    }
}
