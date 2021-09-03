using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.FinancialYear
{
    [Service]
    public class AddFinancialYear
    {
        private IFinancialYearRepository _financialYearRepository;
        public AddFinancialYear(IFinancialYearRepository financialYearRepository)
        {
            _financialYearRepository = financialYearRepository;
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
            Guid ProductId = Guid.NewGuid();
            var financialYear = new Aow.Infrastructure.Domain.FinancialYear
            {
                Id = ProductId,
                Name = request.Name,
                Start = Convert.ToDateTime(request.Start),
                End = Convert.ToDateTime(request.End),
                CompanyId = Guid.Parse(request.CompanyId)
            };

            _financialYearRepository.Create(financialYear);
            int i = await _financialYearRepository.Save();
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
    }
}
