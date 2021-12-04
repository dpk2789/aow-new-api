using Aow.Infrastructure.IRepositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aow.Services.Manufacture
{
    [Service]
    public class AddManufacture
    {
        private IRepositoryWrapper _repoWrapper;
        public AddManufacture(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class AddManufactureRequest
        {
            public string FinancialYearId { get; set; }
            public string data { get; set; }
            public string data2 { get; set; }
            public string VoucherNumber { get; set; }
            public string Date { get; set; }
            public List<AddInputOutputRequest> StoreItems { get; set; }

        }
        public class AddInputOutputRequest
        {
            public Guid Id { get; set; }
            public Guid? StockItemId { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ConsumedQuantity { get; set; }
            public string Status { get; set; }
            public decimal? Rate { get; set; }
            public string Type { get; set; }
        }
        public class AddManufactureResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<AddManufactureResponse> Do(AddManufactureRequest request)
        {
            try
            {
                Guid manufactureId = Guid.NewGuid();
                Guid fyrId = Guid.Parse(request.FinancialYearId);
                int SrNo = 1;
                var manufacture = new Aow.Infrastructure.Domain.Manufacture
                {
                    Id = manufactureId,
                    Date = Convert.ToDateTime(request.Date),
                    VoucherNumber = request.VoucherNumber,
                    FinancialYearId = fyrId
                };
                _repoWrapper.ManufacturingRepo.Create(manufacture);
                var deserialiseList = JsonConvert.DeserializeObject<List<AddInputOutputRequest>>(request.data);
                var deserialiseListOutput = JsonConvert.DeserializeObject<List<AddInputOutputRequest>>(request.data2);
                //input
                foreach (var input in deserialiseList)
                {
                    var manufacturingItem = new Aow.Infrastructure.Domain.ManufactureItem
                    {
                        Id = Guid.NewGuid(),
                        StockId = input.StockItemId,
                        Quantity = input.Quantity,
                        Type = input.Type,
                        ManufactureId = manufactureId,
                        SrNo = SrNo,
                    };
                    _repoWrapper.ManufacturingItemRepo.Create(manufacturingItem);
                    var storeItem = _repoWrapper.StockRepo.GetStock(input.StockItemId.Value);
                    storeItem.ConsumedQuantity = input.ConsumedQuantity;
                    _repoWrapper.StockRepo.Update(storeItem);
                    SrNo++;
                }
                //output
                foreach (var output in deserialiseListOutput)
                {
                    int x = 0;
                    Guid stockId = Guid.NewGuid();
                    var stockItem = _repoWrapper.StockRepo.GetStock(output.StockItemId.Value);
                    var stockNew = new Aow.Infrastructure.Domain.Stock
                    {
                        Id = stockId,
                        Date = request.Date.ToString(),
                        MRPPerUnit = output.Rate,
                        Quantity = output.Quantity,
                        ProductId = stockItem.ProductId,
                        CreatedDate = DateTime.Now,
                        InOut = "In",
                        Status = "Full",
                        Type = "Manufacture",
                    };
                    _repoWrapper.StockRepo.Create(stockNew);

                    var manufacturingItem = new Aow.Infrastructure.Domain.ManufactureItem
                    {
                        Id = Guid.NewGuid(),
                        StockId = output.StockItemId,
                        Quantity = output.Quantity,
                        Type = output.Type,
                        ManufactureId = manufactureId,
                        SrNo = x
                    };
                    _repoWrapper.ManufacturingItemRepo.Create(manufacturingItem);
                    x++;
                }
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new AddManufactureResponse
                    {
                        Description = "Manufacture Voucher Not SuccessFully Added",
                        Success = false
                    };
                }
                else
                {
                    return new AddManufactureResponse
                    {
                        Id = manufacture.Id,
                        Success = true,
                        Description = "Manufacture Voucher SuccessFully Added"
                    };
                }
            }

            catch (Exception ex)
            {
                return new AddManufactureResponse
                {
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}

