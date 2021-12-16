using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.JournalEntry
{
    [Service]
    public class UpdateJournalEntries
    {
        private IRepositoryWrapper _repoWrapper;
        public UpdateJournalEntries(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class UpdateJournalEntriesRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid ProductCategoryId { get; set; }
        }
        public class UpdateJournalEntriesResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<UpdateJournalEntriesResponse> Do(UpdateJournalEntriesRequest request)
        {
            try
            {
                var product = _repoWrapper.ProductRepo.GetProduct(request.Id);
                if (product == null)
                {
                    return null;
                }
                product.Name = request.Name;
                product.ProductCategoryId = request.ProductCategoryId;
                _repoWrapper.ProductRepo.Update(product);

                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new UpdateJournalEntriesResponse
                    {
                        Name = request.Name,
                        Success = false,
                        Description = "Some Error Occur"
                    };
                }
                else
                {
                    return new UpdateJournalEntriesResponse
                    {
                        Id = product.Id,
                        Name = request.Name,
                        Success = true,
                        Description = "Successfully Updated Product!!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new UpdateJournalEntriesResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
