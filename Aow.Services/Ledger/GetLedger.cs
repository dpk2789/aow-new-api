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
            public string RegTaxNumber { get; set; }
            public string PANNumber { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string LandMark { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
            public string ZipCode { get; set; }
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
                AddressLine1 = ledger.AddressLine1,
                AddressLine2 = ledger.AddressLine2,
                Country = ledger.Country,
                State = ledger.State,
                City = ledger.City,
                ZipCode = ledger.ZipCode,
                RegTaxNumber = ledger.RegTaxNumber,
            };

            return getCompanyResponse;
        }

    }
}
