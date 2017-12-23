using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Billing.Domain
{
    public class Bill
    {
        public Bill(string id, IEnumerable<Line> lines)
        {
            Id = id;
            Lines = lines;
        }

        public string Id { get; }
        public IEnumerable<Line> Lines { get; }
        public decimal Total => Lines.Sum(l => l.Amount);
    }
}
