
using Aow.Infrastructure.Common;
using System;

namespace AowCore.Domain
{
    public class Product : AuditableEntity<Guid>
    {      

        public string ItemType { get; set; }
        public string TaxType { get; set; }
        public string Name { get; set; }

        public string ProductTaxCode { get; set; }
        public bool Is_Taxable { get; set; }      

        public Guid ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }    
       

        public Guid? LedgerId { get; set; }
        public virtual Ledger Ledger { get; set; }
 
    }
}
