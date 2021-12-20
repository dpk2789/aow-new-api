using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Ledger
{
    [Service]
    public class UpdateLedger
    {
        private IRepositoryWrapper _repoWrapper;
        public UpdateLedger(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class UpdateLedgerRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string RegTaxNumber { get; set; }
            public string PANNumber { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
            public string ZipCode { get; set; }
            public Guid LedgerCategoryId { get; set; }
        }
        public class UpdateLedgerResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<UpdateLedgerResponse> Do(UpdateLedgerRequest request)
        {
            try
            {
                var ledger = _repoWrapper.LedgerRepositoryRepo.GetLedger(request.Id);
                if (ledger == null)
                {
                    return null;
                }
                ledger.Name = request.Name;
                ledger.LedgerCategoryId = request.LedgerCategoryId;
                ledger.AddressLine1 = request.AddressLine1;
                ledger.AddressLine2 = request.AddressLine2;
                ledger.City = request.City;
                ledger.State = request.State;
                ledger.Country = request.Country;
                ledger.ZipCode = request.ZipCode;
                ledger.RegTaxNumber = request.RegTaxNumber;
                _repoWrapper.LedgerRepositoryRepo.Update(ledger);

                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new UpdateLedgerResponse
                    {
                        Name = request.Name,
                        Success = false,
                        Description = "Some Error Occur"
                    };
                }
                else
                {
                    return new UpdateLedgerResponse
                    {
                        Id = ledger.Id,
                        Name = request.Name,
                        Success = true,
                        Description = "Successfully Updated Ledger Category!!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new UpdateLedgerResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
