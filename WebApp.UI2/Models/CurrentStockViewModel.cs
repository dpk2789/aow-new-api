using System;
using System.Collections.Generic;

namespace WebApp.UI2.Models
{
    public class CurrentStockViewModelJson
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string VoucherNumber { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? MRPPerUnit { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? ItemAmount { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid? VoucherItemId { get; set; }
        public List<GetStockProductVariant> StockProductVariants { get; set; }
    }
    public class GetStockProductVariant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string VoucherNumber { get; set; }
        public string ModelNumber { get; set; }
        public string Size { get; set; }
        public string UniqueNumber { get; set; }
        public string Status { get; set; }
        public decimal? MRPPerUnit { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? ConsumedQuantity { get; set; }
        public decimal? ItemAmount { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid? VoucherItemId { get; set; }       
    }
}
