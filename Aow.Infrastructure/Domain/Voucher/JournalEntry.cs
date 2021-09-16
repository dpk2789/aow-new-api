
using Aow.Infrastructure.Common;
using System;



namespace Aow.Infrastructure.Domain
{
    public class JournalEntry : AuditableEntity<Guid>
    {
        public string VoucherName { get; set; }
        public string VoucherNumber { get; set; }
        public DateTime Date { get; set; }      
      
        public int? SrNo { get; set; }
        public bool? OnRecord { get; set; }
        public Guid? RefAccountId { get; set; }
        public string AccountType { get; set; }        
        public string CrDrType { get; set; }
        public decimal? CreditAmount { get; set; }
        public decimal? DebitAmount { get; set; }
        public string Note { get; set; }
        public Guid LedgerId { get; set; }
        public virtual Ledger Ledger { get; set; }
        public Guid VoucherId { get; set; }
        public virtual Voucher Vouchers { get; set; }
        //public Guid? FinancialYearId { get; set; }
        //public virtual FinancialYear FinancialYear { get; set; }

    }
}
