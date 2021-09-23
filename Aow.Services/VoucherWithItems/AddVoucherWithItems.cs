﻿using Aow.Infrastructure.IRepositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aow.Services.VoucherWithItems
{
    [Service]
    public class AddVoucherWithItems
    {
        private IRepositoryWrapper _repoWrapper;
        public AddVoucherWithItems(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class AddVoucherWithItemsRequest
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
            public IList<AddVoucherItemsRequest> VoucherItems { get; set; }
        }
        public class AddVoucherItemsRequest
        {
            public string CrDrType { get; set; }
            public Guid LedgerId { get; set; }
            public string InvoiceAccountName { get; set; }
            public string AccountName { get; set; }
            public string RootCategory { get; set; }
            public decimal? CreditAmount { get; set; }
            public decimal? DebitAmount { get; set; }
        }
        public class AddVoucherWithItemsResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<AddVoucherWithItemsResponse> Do(AddVoucherWithItemsRequest request)
        {
            try
            {
                Guid fyrId = Guid.Parse(request.FinancialYearId);
                var fyr = _repoWrapper.FinancialYearRepo.GetFinancialYear(fyrId);
                var date = Convert.ToDateTime(request.Date);

                if (fyr.IsLocked == false && fyr.Start <= date && fyr.End >= date)
                {
                    Guid voucherId = Guid.NewGuid();
                  
                    int SrNo = 1;
                    var voucher = new Aow.Infrastructure.Domain.Voucher
                    {
                        Id = voucherId,
                        VoucherName = request.voucherName
                    };
                    voucher.Date = Convert.ToDateTime(request.Date);
                    voucher.VoucherNumber = request.Invoice;
                    voucher.FinancialYearId = fyrId;
                    var deserialiseList = JsonConvert.DeserializeObject<List<AddVoucherItemsRequest>>(request.data);
                    _repoWrapper.VoucherRepo.Create(voucher);
                    foreach (var item in deserialiseList)
                    {
                        var journalEntry = new Aow.Infrastructure.Domain.JournalEntry
                        {
                            Id = Guid.NewGuid(),
                            Date = Convert.ToDateTime(request.Date),
                            VoucherName = request.voucherName,
                            SrNo = SrNo,
                            VoucherNumber = request.Invoice,
                            //dailyAccounts.Is_Item = false;
                            VoucherId = voucherId,
                            LedgerId = item.LedgerId
                        };
                        if (item.CrDrType == "Cr")
                        {
                            journalEntry.CrDrType = "Cr";
                            journalEntry.CreditAmount = item.CreditAmount.Value;
                        }
                        else
                        {
                            journalEntry.CrDrType = "Dr";
                            journalEntry.DebitAmount = item.DebitAmount.Value;
                        }

                        _repoWrapper.JournalEntryRepo.Create(journalEntry);
                        SrNo++;
                    }
                    int i = await _repoWrapper.SaveNew();
                    if (i <= 0)
                    {
                        return new AddVoucherWithItemsResponse
                        {
                            Name = request.voucherName,
                            Success = false
                        };
                    }
                    else
                    {
                        return new AddVoucherWithItemsResponse
                        {
                            Id = voucher.Id,
                            Name = request.voucherName,
                            Success = true,
                            Description = "Journal Entry Added SuccessFully Added"
                        };
                    }                
                   

                }
                return new AddVoucherWithItemsResponse
                {                   
                    Name = request.voucherName,
                    Success = true,
                    Description = "Journal Entry Added SuccessFully Added"
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
