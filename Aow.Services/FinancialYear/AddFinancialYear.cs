using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.FinancialYear
{
    [Service]
    public class AddFinancialYear
    {
        private IRepositoryWrapper _repoWrapper;
        public AddFinancialYear(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class AddFinancialYearRequest
        {
            public string Name { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public string CompanyId { get; set; }
        }
        public class AddFinancialYearResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }
        public async Task<AddFinancialYearResponse> Do(AddFinancialYearRequest request)
        {
            try
            {              
                Guid ProductId = Guid.NewGuid();
                Guid cmpId = Guid.Parse(request.CompanyId);              
                var financialYear = new Aow.Infrastructure.Domain.FinancialYear
                {
                    Id = ProductId,
                    Name = request.Name,
                    Start = Convert.ToDateTime(request.Start),
                    End = Convert.ToDateTime(request.End),
                    CompanyId = cmpId,
                    IsActive = true
                };

                _repoWrapper.FinancialYearRepo.Create(financialYear);
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new AddFinancialYearResponse
                    {
                        Name = request.Name,
                        Success = false
                    };
                }
                else
                {
                    return new AddFinancialYearResponse
                    {
                        Id = financialYear.Id,
                        Name = request.Name,
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new AddFinancialYearResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
