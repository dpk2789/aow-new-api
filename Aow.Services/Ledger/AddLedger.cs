using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Ledger
{
    [Service]
    public class AddLedger
    {
        private IRepositoryWrapper _repoWrapper;
        public AddLedger(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class AddLedgerRequest
        {
            public string Name { get; set; }
            public Guid CategoryId { get; set; }
        }
        public class AddLedgerResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<AddLedgerResponse> Do(AddLedgerRequest request)
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
                    return new AddLedgerResponse
                    {
                        Name = request.Name,
                        Success = false
                    };
                }
                else
                {
                    return new AddLedgerResponse
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
                return new AddLedgerResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
