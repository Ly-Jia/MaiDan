using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Billing.Domain
{
    public class Bill
    {
        public Bill(int id, IList<Line> lines, decimal total)
        {
            Id = id;
            Lines = lines;
            Total = total;
        }
        
        public int Id { get; }
        public IList<Line> Lines { get; }
        public decimal Total { get; }

        public Dictionary<decimal, decimal> Taxes
        {
            get
            {
                return Lines.GroupBy(l => l.Tax.CurrentRate)
                    .Select(g => new KeyValuePair<decimal, decimal>(g.First().Tax.CurrentRate.Value, g.Sum(x => x.TaxAmount)))
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
        }
    }
}
