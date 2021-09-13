using Aow.Infrastructure.IRepositories;
using System;

namespace Aow.Services.Ledger
{
    [Service]
    public class GetLedger
    {
        private IRepositoryWrapper _repoWrapper;
        public GetLedger(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetLedgerResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid LedgerCategoryId { get; set; }
        }

        public GetLedgerResponse Do(Guid id)
        {
            var ledger = _repoWrapper.LedgerRepositoryRepo.GetLedger(id);
            GetLedgerResponse getCompanyResponse = new GetLedgerResponse
            {
                Id = ledger.Id,
                LedgerCategoryId = ledger.LedgerCategoryId,
                Name = ledger.Name,
            };

            return getCompanyResponse;
        }

    }
}
