using Aow.Infrastructure.IRepositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Services.VoucherInvoice
{
    [Service]
    public class UpdateVoucherWithItems
    {
        private IRepositoryWrapper _repoWrapper;
        public UpdateVoucherWithItems(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class UpdateVoucherInvoiceRequest
        {
            public Guid Id { get; set; }
            public string FinancialYearId { get; set; }
            public string voucherName { get; set; }
            public string data { get; set; }
            public string data2 { get; set; }
            public string Invoice { get; set; }
            public int? termsDays { get; set; }
            public string AccountId { get; set; }
            public string Total { get; set; }
            public string Note { get; set; }
            public string buttonValue { get; set; }
            public string Date { get; set; }
            public IList<UpdateVoucherInvoiceItemsRequest> VoucherItems { get; set; }
        }
        public class UpdateVoucherInvoiceItemsRequest
        {
            public Guid Id { get; set; }
            public int? SrNo { get; set; }
            public string Name { get; set; }
            public string ItemType { get; set; }
            public string ItemTaxCode { get; set; }
            public string Percent { get; set; }
            public string Description { get; set; }
            public decimal? ItemAmount { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal Price { get; set; }
            public Guid LedgerId { get; set; }
            public Guid ProductId { get; set; }
        }

        public class UpdateVoucherInvoiceSundryItems
        {
            public Guid Id { get; set; }
            public int? SrNo { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Percent { get; set; }
            public string Description { get; set; }
            public decimal? ItemAmount { get; set; }
            public Guid ProductId { get; set; }
        }
        public class UpdateVoucherWithItemsResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<UpdateVoucherWithItemsResponse> Do(UpdateVoucherInvoiceRequest request)
        {
            try
            {
                Guid fyrId = Guid.Parse(request.FinancialYearId);
                var fyr = _repoWrapper.FinancialYearRepo.GetFinancialYear(fyrId);
                var date = Convert.ToDateTime(request.Date);
                int srno = 1;
                int srnoItem = 1;
                decimal itemTotal = 0;
                var updateVoucher =  await _repoWrapper.VoucherRepo.GetVoucher(request.Id);
                if (updateVoucher != null)
                {                    
                    if (updateVoucher.JournalEntries != null)
                    {
                        var journalEntries = updateVoucher.JournalEntries.ToList();
                        if (journalEntries.Count != 0)
                        {
                            foreach (var account in journalEntries)
                            {
                                _repoWrapper.JournalEntryRepo.Delete(account);                              
                            }
                        }
                    }
                    if (updateVoucher.VoucherItems != null)
                    {
                        var voucherItems = updateVoucher.VoucherItems.ToList();
                        if (voucherItems.Count != 0)
                        {
                            foreach (var account in voucherItems)
                            {
                                _repoWrapper.VoucherItemRepo.Delete(account);
                            }
                        }
                    }
                    if (updateVoucher.VoucherSundryItems != null)
                    {
                        var voucherSundryItems = updateVoucher.VoucherSundryItems.ToList();
                        if (voucherSundryItems.Count != 0)
                        {
                            foreach (var account in voucherSundryItems)
                            {
                                _repoWrapper.VoucherSundryItemRepo.Delete(account);
                            }
                        }
                    }
                }
                int SrNo = 1;
                var voucher = new Aow.Infrastructure.Domain.Voucher
                {
                    Id = updateVoucher.Id,
                    VoucherName = request.voucherName,
                    Date = date,
                    VoucherNumber = request.Invoice,
                    FinancialYearId = fyrId
                };
                _repoWrapper.VoucherRepo.Create(voucher);
                Aow.Infrastructure.Domain.JournalEntry jEntryDebit = new Aow.Infrastructure.Domain.JournalEntry
                {
                    Id = Guid.NewGuid(),
                    VoucherId = updateVoucher.Id,
                    VoucherName = request.voucherName,
                    VoucherNumber = request.Invoice,
                    Date = date,
                    SrNo = srno,
                    LedgerId = Guid.Parse(request.AccountId),
                    CrDrType = "Dr",
                    DebitAmount = Convert.ToDecimal(request.Total)
                };
                _repoWrapper.JournalEntryRepo.Create(jEntryDebit);
                if (request.data != null)
                {
                    var deserialiseList = JsonConvert.DeserializeObject<List<UpdateVoucherInvoiceItemsRequest>>(request.data);

                    foreach (var item in deserialiseList)
                    {
                        Aow.Infrastructure.Domain.VoucherItem voucherItem = new Aow.Infrastructure.Domain.VoucherItem();
                        voucherItem.Id = Guid.NewGuid();
                        voucherItem.SrNo = srnoItem;
                        voucherItem.Description = item.Description;
                        voucherItem.MRPPerUnit = item.MRPPerUnit;
                        voucherItem.Price = item.MRPPerUnit.Value;
                        voucherItem.Quantity = item.Quantity;
                        voucherItem.ProductId = item.ProductId;
                        voucherItem.ItemAmount = item.ItemAmount;
                        itemTotal = itemTotal + item.ItemAmount.Value;
                        voucherItem.VoucherId = updateVoucher.Id;
                        _repoWrapper.VoucherItemRepo.Create(voucherItem);
                        srnoItem++;

                        var ledger = _repoWrapper.LedgerRepositoryRepo.GetLedgerByName(fyr.CompanyId, "Sales Account");

                        Aow.Infrastructure.Domain.JournalEntry jEntryCredit = new Aow.Infrastructure.Domain.JournalEntry
                        {
                            Id = Guid.NewGuid(),
                            VoucherId = updateVoucher.Id,
                            VoucherName = request.voucherName,
                            Date = date,
                            SrNo = srno + 1,
                            LedgerId = ledger.Id,
                            CrDrType = "Cr",
                            VoucherNumber = request.Invoice,
                            CreditAmount = itemTotal
                        };
                        _repoWrapper.JournalEntryRepo.Create(jEntryCredit);
                        SrNo++;
                    }
                }

                if (request.data2 != null)
                {
                    var deserialiseList = JsonConvert.DeserializeObject<List<UpdateVoucherInvoiceSundryItems>>(request.data2);
                    foreach (var sundryItem in deserialiseList)
                    {
                        var product = _repoWrapper.ProductRepo.GetProduct(sundryItem.ProductId);
                        Aow.Infrastructure.Domain.VoucherSundryItem voucherSundryItem = new Aow.Infrastructure.Domain.VoucherSundryItem();
                        voucherSundryItem.Id = Guid.NewGuid();
                        voucherSundryItem.SrNo = srno;
                        voucherSundryItem.ProductId = sundryItem.ProductId;
                        voucherSundryItem.Percent = sundryItem.Percent;
                        voucherSundryItem.ItemAmount = sundryItem.ItemAmount;
                        voucherSundryItem.VoucherId = updateVoucher.Id;
                        _repoWrapper.VoucherSundryItemRepo.Create(voucherSundryItem);
                        srno++;
                        Aow.Infrastructure.Domain.JournalEntry jEntryCreditTax = new Aow.Infrastructure.Domain.JournalEntry
                        {
                            Id = Guid.NewGuid(),
                            VoucherId = updateVoucher.Id,
                            VoucherName = request.voucherName,
                            Date = date,
                            SrNo = srno + 1,
                            LedgerId = product.LedgerId.Value,
                            CrDrType = "Cr",
                            VoucherNumber = request.Invoice,
                            CreditAmount = sundryItem.ItemAmount
                        };
                        _repoWrapper.JournalEntryRepo.Create(jEntryCreditTax);
                    }
                }

                int i = await _repoWrapper.SaveNew();
                if (i > 0)
                {
                    return new UpdateVoucherWithItemsResponse
                    {
                        Id = voucher.Id,
                        Name = request.voucherName,
                        Success = true,
                        Description = "Journal Entry Added SuccessFully Added"
                    };
                }
                //}
                return new UpdateVoucherWithItemsResponse
                {
                    Name = request.voucherName,
                    Success = false,
                    Description = "Journal Entry Not Added"
                };

            }
            catch (Exception ex)
            {
                return new UpdateVoucherWithItemsResponse
                {
                    Name = request.voucherName,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
