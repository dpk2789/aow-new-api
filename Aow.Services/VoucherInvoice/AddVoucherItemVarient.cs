using Aow.Infrastructure.IRepositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Services.VoucherInvoice
{
    [Service]
    public class AddVoucherItemVarient
    {
        private IRepositoryWrapper _repoWrapper;
        public AddVoucherItemVarient(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class AddVoucherItemRequest
        {
            public Guid VoucherItemId { get; set; }
            public string voucherName { get; set; }
            public string data { get; set; }
            public IList<AddVoucherItemVarientRequest> Varients { get; set; }
        }

        public class AddVoucherItemVarientRequest
        {
            public Guid Id { get; set; }
            public int? SrNo { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal? ItemAmount { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal Price { get; set; }
            public Guid VarientId { get; set; }
        }
        public class AddVoucherItemResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }
        public async Task<AddVoucherItemResponse> Do(AddVoucherItemRequest request)
        {
            try
            {
                var voucherItem = await _repoWrapper.VoucherItemRepo.GetVoucherItem(request.VoucherItemId);
                int srnoItem = 1;
                if (voucherItem.VoucherItemVariants != null)
                {
                    var varients = voucherItem.VoucherItemVariants.ToList();
                    if (varients.Count != 0)
                    {
                        foreach (var account in varients)
                        {
                            _repoWrapper.VoucherItemVarientRepo.Delete(account);
                        }
                    }
                }
                if (request.data != null)
                {
                    var deserialiseList = JsonConvert.DeserializeObject<List<AddVoucherItemVarientRequest>>(request.data);

                    foreach (var item in deserialiseList)
                    {
                        var varient = new Aow.Infrastructure.Domain.VoucherItemVariant
                        {
                            Id = Guid.NewGuid(),
                            SrNo = srnoItem,
                            Name = item.Name,
                            Description = item.Description,
                            MRPPerUnit = item.MRPPerUnit,
                            UnitQuantity = item.Quantity,
                            ItemAmount = item.ItemAmount,
                            VoucherItemId = voucherItem.Id,
                            ProductVariantId = item.VarientId
                        };
                        _repoWrapper.VoucherItemVarientRepo.Create(varient);
                        var retriveStock = await _repoWrapper.StockRepo.GetStockByVoucherItemId(item.Id);
                        if (retriveStock != null)
                        {
                            foreach (var stock in retriveStock)
                            {
                                _repoWrapper.StockRepo.Delete(stock);
                            }
                        }
                        var getStock = retriveStock.FirstOrDefault();
                        var stockVarient = new Aow.Infrastructure.Domain.StockProductVariant
                        {
                            Id = Guid.NewGuid(),
                            MRPPerUnit = item.MRPPerUnit,
                            Price = item.MRPPerUnit.Value,
                            Quantity = item.Quantity,
                            ProductVariantId = item.VarientId,
                            ItemAmount = item.ItemAmount,
                            StockId = getStock.Id
                        };
                        _repoWrapper.StockVarientRepo.Create(stockVarient);
                        srnoItem++;
                    }
                }

                int i = await _repoWrapper.SaveNew();
                if (i > 0)
                {
                    return new AddVoucherItemResponse
                    {
                        Id = voucherItem.Id,
                        Name = request.voucherName,
                        Success = true,
                        Description = "Journal Entry Added SuccessFully Added"
                    };
                }
                //}
                return new AddVoucherItemResponse
                {
                    Name = request.voucherName,
                    Success = false,
                    Description = "Journal Entry Not Added"
                };

            }
            catch (Exception ex)
            {
                return new AddVoucherItemResponse
                {
                    Name = request.voucherName,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
