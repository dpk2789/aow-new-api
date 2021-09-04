using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.FinancialYear
{
    [Service]
    public class UpdateFinancialYear
    {
        private IRepositoryWrapper _repoWrapper;
        public UpdateFinancialYear(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class UpdateFinancialYearRequest
        {
            public string Name { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public string CompanyId { get; set; }
        }
        public class UpdateFinancialYearResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }
        public async Task<UpdateFinancialYearResponse> Do(UpdateFinancialYearRequest request)
        {
            try
            {
                Guid ProductId = Guid.NewGuid();
               
                var financialYear = new Aow.Infrastructure.Domain.FinancialYear
                {
                    Id = ProductId,
                    Name = request.Name,
                    Start = Convert.ToDateTime(request.Start),
                    End = Convert.ToDateTime(request.End),                  
                    IsActive = true
                };

                _repoWrapper.FinancialYearRepo.Update(financialYear);
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new UpdateFinancialYearResponse
                    {
                        Name = request.Name,
                        Success = false
                    };
                }
                else
                {
                    return new UpdateFinancialYearResponse
                    {
                        Id = financialYear.Id,
                        Name = request.Name,
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new UpdateFinancialYearResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
