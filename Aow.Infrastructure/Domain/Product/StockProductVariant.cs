using Aow.Infrastructure.Common;
using System;

namespace Aow.Infrastructure.Domain
{
    internal class StockProductVariant : Entity<Guid>
    {
        public string Name { get; set; }
        public string ModelNumber { get; set; }
        public string Discription { get; set; }
        public string Size { get; set; }
        public decimal? SalePrice { get; set; }
        public string ItemType { get; set; }
        public string TaxType { get; set; }
        public bool Is_Taxable { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Products { get; set; }
    }
}
