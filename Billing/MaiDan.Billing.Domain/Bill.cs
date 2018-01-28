using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Billing.Domain
{
    public class Bill
    {
        public Bill(int id, IList<Line> lines)
        {
            Id = id;
            Lines = lines;
        }
        
        public int Id { get; }
        public IList<Line> Lines { get; }
        public decimal Total => Lines.Sum(l => l.Amount);
    }
}
