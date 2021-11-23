using Aow.Infrastructure.Common;
using System;

namespace Aow.Infrastructure.Domain
{
    public class StockProductVariant : Entity<Guid>
    {
        public Guid? VoucherItemVarientId { get; set; }
        public string UniqueNumber { get; set; }
        public string Status { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? MRPPerUnit { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? ItemAmount { get; set; }
        public decimal Price { get; set; }       
        public Guid? ProductVariantId { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
        public Guid StockId { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
