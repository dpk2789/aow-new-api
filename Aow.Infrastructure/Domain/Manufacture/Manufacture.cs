using System.Collections.Generic;

namespace Aow.Infrastructure.Domain
{
    public class Manufacture
    {
        public List<InputVarients> InputVarients { get; set; }
        public List<StockProductVariant> OutputVarients { get; set; }
    }
}
