using Aow.Infrastructure.IRepositories;
using System;
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
        public class AddJournalEntryRequest
        {
            public string Name { get; set; }
            public Guid CategoryId { get; set; }
        }
        public class AddJournalEntryResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<AddJournalEntryResponse> Do(AddJournalEntryRequest request)
        {
            try
            {
                Guid ProductId = Guid.NewGuid();
                var ledger = new Aow.Infrastructure.Domain.Ledger
                {
                    Id = ProductId,
                    Name = request.Name,
                    LedgerCategoryId = request.CategoryId,
                };

                _repoWrapper.LedgerRepositoryRepo.Create(ledger);
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
                        Id = ledger.Id,
                        Name = request.Name,
                        Success = true,
                        Description = "Product Category SuccessFully Added"
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
