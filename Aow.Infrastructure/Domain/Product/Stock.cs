﻿using Aow.Infrastructure.Common;
using System;
using System.Collections.Generic;

namespace Aow.Infrastructure.Domain
{
    public class Stock : AuditableEntity<Guid>
    {
        public Guid? VoucherItemId { get; set; }
        public decimal? MRPPerUnit { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? ItemAmount { get; set; }   
        public decimal Price { get; set; }
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public List<StockProductVariant> StockProductVariants { get; set; }
    }
}
