using Aow.Infrastructure.Common;
using System;
using System.Collections.Generic;

namespace Aow.Infrastructure.Domain
{
    public class Manufacture : AuditableEntity<Guid>
    {
        public DateTime Date { get; set; }
        public List<InputVarients> InputVarients { get; set; }
        public List<OutputVarients> OutputVarients { get; set; }
    }
}
