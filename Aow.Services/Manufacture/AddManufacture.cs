﻿using Aow.Infrastructure.IRepositories;
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
            public DateTime Date { get; set; }
            public List<AddInputOutputRequest> StoreVairents { get; set; }

        }
        public class AddInputOutputRequest
        {
            public Guid Id { get; set; }
            public decimal? Quantity { get; set; }
            public string Type { get; set; }
            public Guid? StockProductVariantId { get; set; }
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
                Guid voucherId = Guid.NewGuid();
                Guid fyrId = Guid.Parse(request.FinancialYearId);
                int SrNo = 1;
                var manufacture = new Aow.Infrastructure.Domain.Manufacture
                {
                    Id = voucherId,
                    Date = request.Date,
                    VoucherNumber = request.VoucherNumber,
                    FinancialYearId = fyrId

                };

                var deserialiseList = JsonConvert.DeserializeObject<List<AddInputOutputRequest>>(request.data);
                _repoWrapper.ManufacturingRepo.Create(manufacture);
                foreach (var item in deserialiseList)
                {
                    var manufacturingVarient = new Aow.Infrastructure.Domain.ManufacturingVarients
                    {
                        Id = Guid.NewGuid(),
                        StockProductVariantId = item.Id,
                        Quantity = item.Quantity,
                        Type = item.Type,
                        ManufactureId = voucherId
                    };

                    _repoWrapper.ManufactureVarientsRepo.Create(manufacturingVarient);
                    SrNo++;
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

