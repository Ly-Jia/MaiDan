using System.Collections.Generic;

namespace MaiDan.Billing.Domain
{
    public class Bill
    {
        public Bill(int id, IList<Line> lines, Dictionary<Discount, decimal> discounts, decimal total, IList<BillTax> taxes)
        {
            Id = id;
            Lines = lines;
            Discounts = discounts;
            Total = total;
            Taxes = taxes;
        }
        
        public int Id { get; }
        public IList<Line> Lines { get; }
        public Dictionary<Discount, decimal> Discounts { get; }
        public decimal Total { get; }
        public IList<BillTax> Taxes { get; }
    }
}
