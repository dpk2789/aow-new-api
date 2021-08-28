using Aow.Infrastructure.Common;
using System;
using System.Collections.Generic;

namespace Aow.Infrastructure.Domain
{
    public class Company : AuditableEntity<Guid>
    {        
        public string Name { get; set; }
        public virtual IList<FinancialYear> FinancialYears { get; set; }
        public virtual IList<AppUserCompany> AppUserCompanies { get; set; }
    }
}
