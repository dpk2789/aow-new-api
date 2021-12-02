using System;
using System.Collections.Generic;

namespace Aow.Infrastructure.Domain
{
    public class ManufactureItem
    {
        public Guid Id { get; set; }
        public int? SrNo { get; set; }
        public decimal? Quantity { get; set; }
        public string Type { get; set; }
        public Guid? StockId { get; set; }
        public virtual Stock Stock { get; set; }
        public Guid ManufactureId { get; set; }
        public virtual Manufacture Manufacture { get; set; }
        public List<ManufacturingVarients> ManufacturingVarients { get; set; }
    }
}
