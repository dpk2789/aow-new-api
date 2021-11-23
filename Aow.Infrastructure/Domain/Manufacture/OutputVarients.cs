using System;
using System.Collections.Generic;
using System.Text;

namespace Aow.Infrastructure.Domain
{
    public class OutputVarients
    {
        public decimal? Quantity { get; set; }
        public Guid? StockProductVariantId { get; set; }
        public virtual StockProductVariant StockProductVariant { get; set; }
    }
}
