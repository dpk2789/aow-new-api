using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Products
{
    [Service]
    public class UpdateProduct
    {
        private IRepositoryWrapper _repoWrapper;
        public UpdateProduct(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        } 
        public class UpdateProductRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public string CompanyId { get; set; }
        }
        public class UpdateProductResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<UpdateProductResponse> Do(UpdateProductRequest request)
        {
            try
            {
                var financialYear = _repoWrapper.FinancialYearRepo.GetFinancialYear(request.Id);
                if (financialYear == null)
                {
                    return null;
                }
                financialYear.Name = request.Name;
                financialYear.Start = Convert.ToDateTime(request.Start);
                DateTime dt = Convert.ToDateTime(request.End);
                //DateTime dt2;
                //var success = DateTimeOffset.TryParse(request.End, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var result);
                //dt2 = result.UtcDateTime;
                //var date2 = result.ToLocalTime();
                financialYear.End = dt;
                _repoWrapper.FinancialYearRepo.Update(financialYear);

                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new UpdateProductResponse
                    {
                        Name = request.Name,
                        Success = false
                    };
                }
                else
                {
                    return new UpdateProductResponse
                    {
                        Id = financialYear.Id,
                        Name = request.Name,
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new UpdateProductResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
