using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Billing.Domain
{
    public class Bill
    {
        public Bill(int id, IEnumerable<Line> lines)
        {
            Id = id;
            Lines = lines;
        }
        
        public int Id { get; }
        public IEnumerable<Line> Lines { get; }
        public decimal Total => Lines.Sum(l => l.Amount);
    }
}
