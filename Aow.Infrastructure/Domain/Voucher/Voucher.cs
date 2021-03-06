
using Aow.Infrastructure.Common;
using System;
using System.Collections.Generic;

namespace Aow.Infrastructure.Domain
{
    public class Voucher : AuditableEntity<Guid>
    {
        public string VoucherNumber { get; set; }
        public int VoucherTypeId { get; set; }
        public string VoucherName { get; set; }
        public DateTime Date { get; set; }
        public string RefId { get; set; }
        public string Note { get; set; }
        public decimal Total { get; set; }
        public bool? Type { get; set; }
        public virtual IList<JournalEntry> JournalEntries { get; set; }
        public virtual IList<VoucherItem> VoucherItems { get; set; }
        public virtual IList<VoucherSundryItem> VoucherSundryItems { get; set; }
        public virtual IList<TransporterDetail> TransporterDetails { get; set; }

        public Guid? FinancialYearId { get; set; }
        public virtual FinancialYear FinancialYear { get; set; }
    }
}
