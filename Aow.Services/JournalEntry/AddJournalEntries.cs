using Aow.Infrastructure.IRepositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aow.Services.JournalEntry
{
    [Service]
    public class AddJournalEntries
    {
        private IRepositoryWrapper _repoWrapper;
        public AddJournalEntries(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class AddJournalEntryVoucherRequest
        {
            //public Guid FinancialYrId { get; set; }
            public string Name { get; set; }
            public string data { get; set; }
            public string Invoice { get; set; }
            public string Date { get; set; }
            public IList<AddJournalEntryRequest> JournalEntries { get; set; }
        }
        public class AddJournalEntryRequest
        {
            public string CrDrType { get; set; }
            public Guid LedgerId { get; set; }
            public string InvoiceAccountName { get; set; }
            public string AccountName { get; set; }
            public string RootCategory { get; set; }
            public decimal? CreditAmount { get; set; }
            public decimal? DebitAmount { get; set; }
        }
        public class AddJournalEntryResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<AddJournalEntryResponse> Do(AddJournalEntryVoucherRequest request)
        {
            try
            {
                Guid voucherId = Guid.NewGuid();
                int SrNo = 1;
                var voucher = new Aow.Infrastructure.Domain.Voucher
                {
                    Id = voucherId,
                    VoucherName = request.Name
                };
               // voucher.FinancialYearId = request.FinancialYrId;
                var deserialiseList = JsonConvert.DeserializeObject<List<AddJournalEntryRequest>>(request.data);
                _repoWrapper.VoucherRepo.Create(voucher);
                foreach (var item in deserialiseList)
                {
                    var journalEntry = new Aow.Infrastructure.Domain.JournalEntry
                    {
                        Id = Guid.NewGuid(),
                        Date = Convert.ToDateTime(request.Date),
                        VoucherName = request.Name,
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
                    return new AddJournalEntryResponse
                    {
                        Name = request.Name,
                        Success = false
                    };
                }
                else
                {
                    return new AddJournalEntryResponse
                    {
                        Id = voucher.Id,
                        Name = request.Name,
                        Success = true,
                        Description = "Journal Entry Added SuccessFully Added"
                    };
                }
            }
            
            catch (Exception ex)
            {
                return new AddJournalEntryResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
