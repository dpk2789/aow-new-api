using Aow.Infrastructure.IRepositories;
using System;

namespace Aow.Services.FinancialYear
{
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
            public string FinancialYearName
            {
                get
                {
                    string financialYearName = string.Format("{0} - {1}", Start.Value.ToString("yyyy-MM-dd"), End.Value.ToString("yyyy-MM-dd")).Trim();
                    return financialYearName == "<br/>" ? string.Empty : financialYearName;
                }
            }
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
