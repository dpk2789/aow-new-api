using Aow.Infrastructure.IRepositories;
using System;

namespace Aow.Services.FinancialYear
{
    [Service]
    public class GetFinancialYear
    {
        private IRepositoryWrapper _repoWrapper;
        public GetFinancialYear(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetFinancialYearResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public DateTime? Start { get; set; }
            public DateTime? End { get; set; }
            public DateTime CreatedDate { get; set; }
            public bool IsActive { get; set; }
            public bool? IsLocked { get; set; }
            public Guid CompanyId { get; set; }
            public bool Success { get; set; }           
        }

     
        public GetFinancialYearResponse Do(Guid id)
        {
            var company = _repoWrapper.FinancialYearRepo.GetFinancialYear(id);
            GetFinancialYearResponse getCompanyResponse = new GetFinancialYearResponse
            {
                Id = company.Id,
                Name = company.Name
            };

            return getCompanyResponse;
        }
    }
}
