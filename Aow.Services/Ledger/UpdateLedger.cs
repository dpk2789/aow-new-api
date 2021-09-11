using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Ledger
{
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
            public string CompanyId { get; set; }
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
                var category = _repoWrapper.ProductCategoryRepo.GetProductCategory(request.Id);
                if (category == null)
                {
                    return null;
                }
                category.Name = request.Name;
                _repoWrapper.ProductCategoryRepo.Update(category);

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
                        Id = category.Id,
                        Name = request.Name,
                        Success = true,
                        Description = "Successfully Updated Product Category!!"
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
