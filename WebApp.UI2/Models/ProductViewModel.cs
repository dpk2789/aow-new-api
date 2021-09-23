using System;

namespace WebApp.UI2.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }     
        public Guid? ProductCategoryId { get; set; }     
        public Guid? ProductId { get; set; }     
        public string Name { get; set; }
        public string Code { get; set; }
        public string ModelNumber { get; set; }
        public string Title { get; set; }
        public string Percent { get; set; }
        public string ProductTaxCode { get; set; }
        public string DiscountType { get; set; } //dynamic amount or %
        public string ItemType { get; set; }
        public string TaxType { get; set; }
        public string ItemTypeId { get; set; }      
        public string CategoryName { get; set; }
        public string AutoGenerateName { get; set; }
        public decimal? SalePrice { get; set; }

    }
}
