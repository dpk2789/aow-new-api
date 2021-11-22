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
                    foreach (var item in voucherItem.VoucherItemVariants)
                    {
                        if (!deserialiseList.Any(x => x.Id == item.Id))
                        {
                            var voucherItemVarient = voucherItem.VoucherItemVariants.FirstOrDefault(x => x.Id == item.Id);
                            if (voucherItemVarient != null)
                            {
                                _repoWrapper.VoucherItemVarientRepo.Delete(voucherItemVarient);
                            }
                            //stock deletion after voucher item delete from ui
                            var retriveStockVarient =  _repoWrapper.StockVarientRepo.GetStockVarientByVoucherVarient(voucherItemVarient.Id);                         
                            _repoWrapper.StockVarientRepo.Delete(retriveStockVarient);
                        }
                    }
                    foreach (var item in deserialiseList)
                    {
                        var retriveStocks = await _repoWrapper.StockRepo.GetStockByVoucherItemId(voucherItem.Id);
                        var getStock = retriveStocks.FirstOrDefault();
                        if (item.Id == Guid.Empty)
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
                            var stockVarientNew = new Aow.Infrastructure.Domain.StockProductVariant
                            {
                                Id = Guid.NewGuid(),
                                MRPPerUnit = item.MRPPerUnit,
                                Price = item.MRPPerUnit.Value,
                                Quantity = item.Quantity,
                                ProductVariantId = item.VarientId,
                                ItemAmount = item.ItemAmount,
                                StockId = getStock.Id,
                                VoucherItemVarientId = varient.Id
                            };
                            _repoWrapper.StockVarientRepo.Create(stockVarientNew);
                        }
                        else
                        {
                            var voucherItemVrient = _repoWrapper.VoucherItemVarientRepo.GetVoucherItemVarient(item.Id);
                            //voucherItemVrient.SrNo = srnoItem;
                            voucherItemVrient.Name = item.Name;
                            voucherItemVrient.Description = item.Description;
                            voucherItemVrient.MRPPerUnit = item.MRPPerUnit;
                            voucherItemVrient.UnitQuantity = item.Quantity;
                            voucherItemVrient.ItemAmount = item.ItemAmount;
                            _repoWrapper.VoucherItemVarientRepo.Update(voucherItemVrient);

                            var stockVarient = _repoWrapper.StockVarientRepo.GetStockVarientByVoucherVarient(voucherItemVrient.Id);
                            if (stockVarient != null)
                            {
                                stockVarient.MRPPerUnit = item.MRPPerUnit;
                                stockVarient.Quantity = item.Quantity;
                                stockVarient.ItemAmount = item.ItemAmount;
                                _repoWrapper.StockVarientRepo.Update(stockVarient);
                            }
                            else
                            {
                                var stockVarientNew = new Aow.Infrastructure.Domain.StockProductVariant
                                {
                                    Id = Guid.NewGuid(),
                                    MRPPerUnit = item.MRPPerUnit,
                                    Price = item.MRPPerUnit.Value,
                                    Quantity = item.Quantity,
                                    ProductVariantId = item.VarientId,
                                    ItemAmount = item.ItemAmount,
                                    StockId = getStock.Id,
                                    VoucherItemVarientId = voucherItemVrient.Id
                                };
                                _repoWrapper.StockVarientRepo.Create(stockVarientNew);
                            }

                        }
                        //if (retriveStocks != null)
                        //{
                        //    foreach (var stockVarient in getStock.StockProductVariants)
                        //    {
                        //        _repoWrapper.StockVarientRepo.Delete(stockVarient);
                        //    }
                        //}

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
