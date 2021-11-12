
using Aow.Infrastructure.Common;
using System;
using System.Collections.Generic;

namespace Aow.Infrastructure.Domain
{
    public class Product : AuditableEntity<Guid>
    {
        public string ItemType { get; set; }
        public string TaxType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Code { get; set; }
        public string ModelNumber { get; set; }
        public string Title { get; set; }
        public string Percent { get; set; }
        public string ProductTaxCode { get; set; }
        public string DiscountType { get; set; } //dynamic amount or %
        
        public string ItemTypeId { get; set; }
        public string CategoryName { get; set; }
        public string AutoGenerateName { get; set; }
        public decimal? Value { get; set; }
        public decimal? SalePrice { get; set; }      
        public bool Is_Taxable { get; set; }      

        public Guid ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }    
       

        public Guid? LedgerId { get; set; }
        public virtual Ledger Ledger { get; set; }
        public List<ProductVariant> ProductVariants { get; set; }

    }
}
