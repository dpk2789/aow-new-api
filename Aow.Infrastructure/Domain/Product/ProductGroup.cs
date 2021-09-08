using Aow.Infrastructure.Common;
using System;
using System.Collections.Generic;

namespace Aow.Infrastructure.Domain
{
    public class ProductGroup : Entity<Guid>
    {
        public string Name { get; set; }
        public string Type { get; set; } 
        public IList<ItemGroupProduct> ItemGroupProducts { get; set; }

    }
}
