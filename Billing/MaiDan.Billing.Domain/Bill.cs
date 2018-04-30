using System.Collections.Generic;

namespace MaiDan.Billing.Domain
{
    public class Bill
    {
        public Bill(int id, IList<Line> lines, decimal total, IList<BillTax> taxes)
        {
            Id = id;
            Lines = lines;
            Total = total;
            Taxes = taxes;
        }
        
        public int Id { get; }
        public IList<Line> Lines { get; }
        public decimal Total { get; }
        public IList<BillTax> Taxes { get; }
    }
}
