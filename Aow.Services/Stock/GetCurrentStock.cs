using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.Store
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
            public string Name { get; set; }
            public string ModelNumber { get; set; }
            public string Discription { get; set; }
            public string Size { get; set; }
            public decimal? SalePrice { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ItemAmount { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public Guid? ProductVariantId { get; set; }
            public string ProductVariantName { get; set; }
        }

        public IEnumerable<GetCurrentStockResponse> Do(PagingParameters pagingParameters, Guid companyId)
        {
            var list = _repoWrapper.StockRepo.GetStocks(pagingParameters, companyId).GetAwaiter().GetResult();
            var newList = list.Select(x => new GetCurrentStockResponse
            {
                Id = x.Id,
                Quantity = x.Quantity,
                Name = x.Product.Name
            });

            return newList;
        }
    }
}
