using Aow.Infrastructure.Common;
using System;


namespace Aow.Infrastructure.Domain
{
    public class VoucherItem : AuditableEntity<Guid>
    {    
        public int? SrNo { get; set; }
        public decimal? DiscountRatePerUnit { get; set; }
        public decimal? MRPPerUnit { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? ItemAmount { get; set; }
        public string Description { get; set; }       
        public decimal Price { get; set; }      
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }     

        public Guid VoucherId { get; set; }
        public virtual Voucher Voucher { get; set; }
       
    }
}

