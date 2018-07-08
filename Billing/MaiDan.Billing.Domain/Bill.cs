using System;
using System.Collections.Generic;

namespace MaiDan.Billing.Domain
{
    public class Bill
    {
        public Bill(int id, DateTime billingDate, IList<Line> lines, Dictionary<Discount, decimal> discounts, decimal total, Dictionary<TaxRate, decimal> taxes, bool closed)
        {
            Id = id;
            BillingDate = billingDate;
            Lines = lines;
            Discounts = discounts;
            Total = total;
            Taxes = taxes;
            Closed = closed;
        }
        
        public int Id { get; }
        public DateTime BillingDate { get; }
        public IList<Line> Lines { get; }
        public Dictionary<Discount, decimal> Discounts { get; }
        public decimal Total { get; }
        public Dictionary<TaxRate, decimal> Taxes { get; }
        public bool Closed { get; private set; }

        public void Close()

        {
            Closed = true;
        }
    }
}
