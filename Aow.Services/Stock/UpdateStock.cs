using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Stock
{
    [Service]
    public class UpdateStock
    {
        private IRepositoryWrapper _repoWrapper;
        public UpdateStock(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class UpdateStockRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public string Quantity { get; set; }
            public decimal? ItemAmount { get; set; }
            public decimal Price { get; set; }
        }
        public class UpdateStockResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<UpdateStockResponse> Do(UpdateStockRequest request)
        {
            try
            {
                var stock = _repoWrapper.StockRepo.GetStock(request.Id);
                if (stock == null)
                {
                    return null;
                }
                stock.Quantity = Convert.ToDecimal(request.Quantity);
                _repoWrapper.StockRepo.Update(stock);

                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new UpdateStockResponse
                    {
                        Name = request.Name,
                        Success = false,
                        Description = "Some Error Occur"
                    };
                }
                else
                {
                    return new UpdateStockResponse
                    {
                        Id = stock.Id,
                        Name = request.Name,
                        Success = true,
                        Description = "Successfully Updated Stock!!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new UpdateStockResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
