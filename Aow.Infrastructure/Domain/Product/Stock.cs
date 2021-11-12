using Aow.Infrastructure.Common;
using System;
using System.Collections.Generic;

namespace Aow.Infrastructure.Domain
{
    public class Stock : AuditableEntity<Guid>
    {
        public string Name { get; set; }
        public string ModelNumber { get; set; }
        public string Discription { get; set; }
        public string Size { get; set; }
        public decimal? SalePrice { get; set; }
        public string ItemType { get; set; }
        public string TaxType { get; set; }
        public bool Is_Taxable { get; set; }
        public decimal? MRPPerUnit { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? ItemAmount { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid? VoucherItemId { get; set; }
        public virtual VoucherItem VoucherItem { get; set; }
        public List<StockProductVariant> StockProductVariants { get; set; }
    }
}
