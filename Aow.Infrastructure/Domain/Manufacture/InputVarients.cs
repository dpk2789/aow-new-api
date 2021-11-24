using System;

namespace Aow.Infrastructure.Domain
{
    public class InputVarients
    {
        public Guid Id { get; set; }
        public decimal? Quantity { get; set; }
        public Guid? StockProductVariantId { get; set; }
        public virtual StockProductVariant StockProductVariant { get; set; }
    }
}
