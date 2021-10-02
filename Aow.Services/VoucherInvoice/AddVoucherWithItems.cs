using Aow.Infrastructure.IRepositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aow.Services.VoucherInvoice
{
    [Service]
    public class AddVoucherWithItems
    {
        private IRepositoryWrapper _repoWrapper;
        public AddVoucherWithItems(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class AddVoucherInvoiceRequest
        {
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
            public IList<AddVoucherInvoiceItemsRequest> VoucherItems { get; set; }
        }
        public class AddVoucherInvoiceItemsRequest
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

        public class AddVoucherInvoiceSundryItems
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
        public class AddVoucherWithItemsResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<AddVoucherWithItemsResponse> Do(AddVoucherInvoiceRequest request)
        {
            try
            {
                Guid fyrId = Guid.Parse(request.FinancialYearId);
                var fyr = _repoWrapper.FinancialYearRepo.GetFinancialYear(fyrId);
                var date = Convert.ToDateTime(request.Date);
                int srno = 1;
                int srnoItem = 1;
                decimal itemTotal = 0;
                //if (fyr.IsLocked == false && fyr.Start <= date && fyr.End >= date)
                //{
                Guid voucherId = Guid.NewGuid();

                int SrNo = 1;
                var voucher = new Aow.Infrastructure.Domain.Voucher
                {
                    Id = voucherId,
                    VoucherName = request.voucherName,
                    Date = date,
                    VoucherNumber = request.Invoice,
                    FinancialYearId = fyrId
                };
                _repoWrapper.VoucherRepo.Create(voucher);
                Aow.Infrastructure.Domain.JournalEntry jEntryDebit = new Aow.Infrastructure.Domain.JournalEntry
                {
                    Id = Guid.NewGuid(),
                    VoucherId = voucherId,
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
                    var deserialiseList = JsonConvert.DeserializeObject<List<AddVoucherInvoiceItemsRequest>>(request.data);

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
                        voucherItem.VoucherId = voucherId;
                        _repoWrapper.VoucherItemRepo.Create(voucherItem);
                        srnoItem++;

                        var ledger = _repoWrapper.LedgerRepositoryRepo.GetLedgerByName(fyr.CompanyId, "Sales Account");

                        Aow.Infrastructure.Domain.JournalEntry jEntryCredit = new Aow.Infrastructure.Domain.JournalEntry
                        {
                            Id = Guid.NewGuid(),
                            VoucherId = voucherId,
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
                    var deserialiseList = JsonConvert.DeserializeObject<List<AddVoucherInvoiceSundryItems>>(request.data2);
                    foreach (var sundryItem in deserialiseList)
                    {
                        var product = _repoWrapper.ProductRepo.GetProduct(sundryItem.ProductId);
                        Aow.Infrastructure.Domain.VoucherSundryItem voucherSundryItem = new Aow.Infrastructure.Domain.VoucherSundryItem();
                        voucherSundryItem.Id = Guid.NewGuid();
                        voucherSundryItem.SrNo = srno;
                        voucherSundryItem.ProductId = sundryItem.ProductId;
                        voucherSundryItem.Percent = sundryItem.Percent;
                        voucherSundryItem.ItemAmount = sundryItem.ItemAmount;
                        voucherSundryItem.VoucherId = voucherId;
                        _repoWrapper.VoucherSundryItemRepo.Create(voucherSundryItem);
                        srno++;
                        Aow.Infrastructure.Domain.JournalEntry jEntryCreditTax = new Aow.Infrastructure.Domain.JournalEntry
                        {
                            Id = Guid.NewGuid(),
                            VoucherId = voucherId,
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
                    return new AddVoucherWithItemsResponse
                    {
                        Id = voucher.Id,
                        Name = request.voucherName,
                        Success = true,
                        Description = "Journal Entry Added SuccessFully Added"
                    };
                }
                //}
                return new AddVoucherWithItemsResponse
                {
                    Name = request.voucherName,
                    Success = false,
                    Description = "Journal Entry Not Added"
                };

            }
            catch (Exception ex)
            {
                return new AddVoucherWithItemsResponse
                {
                    Name = request.voucherName,
                    Success = false,
                    Description = ex.Message
                };
            }
        }

    }
}
