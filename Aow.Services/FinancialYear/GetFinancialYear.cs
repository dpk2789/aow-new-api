using System;

namespace Aow.Services.FinancialYear
{
    public class GetFinancialYear
    {
        public class GetFinancialYearResponse
        {
            public string Name { get; set; }
            public DateTime? Start { get; set; }
            public DateTime? End { get; set; }
            public DateTime CreatedDate { get; set; }
            public bool IsActive { get; set; }
            public bool? IsLocked { get; set; }
            public Guid CompanyId { get; set; }
           
            public string FinancialYearName
            {
                get
                {
                    string financialYearName = string.Format("{0} - {1}", Start.Value.ToString("yyyy-MM-dd"), End.Value.ToString("yyyy-MM-dd")).Trim();
                    return financialYearName == "<br/>" ? string.Empty : financialYearName;
                }
            }
        }
    }
}
