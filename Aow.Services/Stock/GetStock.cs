using Aow.Infrastructure.IRepositories;
using System;

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
            public Guid ProductCategoryId { get; set; }
        }
        public GetStockResponse Do(Guid Id)
        {
            var stock = _repoWrapper.StockRepo.GetStock(Id);
            if (stock == null)
            {
                return null;
            }
            GetStockResponse getProductViewModel = new GetStockResponse
            {
                Id = stock.Id,
                Name = stock.Product.Name,
                Quantity = stock.Quantity
            };
            return getProductViewModel;
        }
    }
}
