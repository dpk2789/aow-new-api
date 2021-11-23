using System;

namespace Aow.Infrastructure.Domain
{
    public class StockVarientHistory
    {
        public decimal? Quantity { get; set; }
        public Guid? StockProductVariantId { get; set; }
        public virtual StockProductVariant StockProductVariant { get; set; }
    }
}
