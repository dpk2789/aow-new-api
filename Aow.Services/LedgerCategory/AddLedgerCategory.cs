using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.LedgerCategory
{
    [Service]
    public class AddLedgerCategory
    {
        private IRepositoryWrapper _repoWrapper;
        public AddLedgerCategory(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class AddLedgerCategoryRequest
        {
            public string Name { get; set; }
            public string CompanyId { get; set; }
        }
        public class AddLedgerCategoryResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<AddLedgerCategoryResponse> Do(AddLedgerCategoryRequest request)
        {
            try
            {
                Guid ProductId = Guid.NewGuid();
                Guid cmpId = Guid.Parse(request.CompanyId);
                var ledgerCategory = new Aow.Infrastructure.Domain.LedgerCategory
                {
                    Id = ProductId,
                    Name = request.Name,
                    CompanyId = cmpId,
                };

                _repoWrapper.LedgerCategoryRepositoryRepo.Create(ledgerCategory);
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new AddLedgerCategoryResponse
                    {
                        Name = request.Name,
                        Success = false
                    };
                }
                else
                {
                    return new AddLedgerCategoryResponse
                    {
                        Id = ledgerCategory.Id,
                        Name = request.Name,
                        Success = true,
                        Description = "Ledger Category SuccessFully Added"
                    };
                }
            }
            catch (Exception ex)
            {
                return new AddLedgerCategoryResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
