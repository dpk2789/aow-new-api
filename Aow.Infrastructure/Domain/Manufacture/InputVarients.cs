using System;

namespace Aow.Infrastructure.Domain
{
    public class ManufacturingVarients
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public decimal? Quantity { get; set; }
        public string Type { get; set; }
        public Guid? StockProductVariantId { get; set; }
        public virtual StockProductVariant StockProductVariant { get; set; }
        public Guid ManufactureId { get; set; }
        public virtual Manufacture Manufacture { get; set; }
    }
}
