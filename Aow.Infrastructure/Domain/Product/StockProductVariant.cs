using Aow.Infrastructure.Common;
using System;

namespace Aow.Infrastructure.Domain
{
    public class StockProductVariant : Entity<Guid>
    {
        public string Name { get; set; }
        public string ModelNumber { get; set; }
        public string Discription { get; set; }
        public string Size { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? MRPPerUnit { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? ItemAmount { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid? ProductVariantId { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }       
    }
}
