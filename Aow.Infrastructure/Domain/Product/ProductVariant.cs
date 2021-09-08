using Aow.Infrastructure.Common;
using System;
using System.Collections.Generic;

namespace Aow.Infrastructure.Domain
{
    public class ProductVariant : AuditableEntity<Guid>
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
        public Guid ProductAttributeOptions1Id { get; set; }
        public Guid ProductAttributeOptions2Id { get; set; }
        public Guid ProductAttributeOptionsId { get; set; }
     
        public List<ProductVariantProductAttributeOption> ProductVariantProductAttributeOptions { get; set; }

    }
}
