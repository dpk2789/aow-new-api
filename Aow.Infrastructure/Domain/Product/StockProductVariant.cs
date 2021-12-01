using Aow.Infrastructure.Common;
using System;
using System.Collections.Generic;

namespace Aow.Infrastructure.Domain
{
    public class StockProductVariant : Entity<Guid>
    {
        public Guid? VoucherItemVarientId { get; set; }
        public string UniqueNumber { get; set; }
        public string InOut { get; set; }
        public string StockInBy { get; set; }
        public string Status { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? MRPPerUnit { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? ConsumedQuantity { get; set; }       
        public decimal? ItemAmount { get; set; }
        public decimal Price { get; set; }       
        public Guid? ProductVariantId { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
        public Guid StockId { get; set; }
        public virtual Stock Stock { get; set; }
        public List<ManufacturingVarients> ManufacturingVarients { get; set; }
    }
}
