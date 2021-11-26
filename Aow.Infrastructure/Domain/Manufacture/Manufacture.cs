using Aow.Infrastructure.Common;
using System;
using System.Collections.Generic;

namespace Aow.Infrastructure.Domain
{
    public class Manufacture : AuditableEntity<Guid>
    {
        public DateTime Date { get; set; }
        public string VoucherNumber { get; set; }
        public List<ManufacturingVarients> ManufacturingVarients { get; set; }
        public Guid? FinancialYearId { get; set; }
        public virtual FinancialYear FinancialYear { get; set; }

    }
}
