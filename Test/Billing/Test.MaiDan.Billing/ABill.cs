using MaiDan.Billing.Domain;
using System;
using System.Collections.Generic;

namespace Test.MaiDan.Billing
{
    /// <summary>
    /// Builder that creates Bill
    /// </summary>
    public class ABill
    {
        private const int DefaultId = 1;
        private readonly int id;
        private DateTime billingDate = DateTime.Now;
        private List<Line> lines = new List<Line>();
        private Dictionary<Discount, decimal> discounts = new Dictionary<Discount, decimal>();
        private decimal total = 0;
        private Dictionary<TaxRate, decimal> taxes = new Dictionary<TaxRate, decimal>();
        private bool closed = false;

        public ABill() : this(DefaultId)
        {
        }

        public ABill(int id)
        {
            this.id = id;
        }

        public ABill With(decimal amount)
        {
            With(lines.Count + 1, amount);
            return this;
        }

        public ABill With(int id, decimal amount)
        {
            With(new Line(id, amount, ATax.DefaultTaxRate));
            return this;
        }

        public ABill With(int id, decimal amount, TaxRate taxRate)
        {
            With(new Line(id, amount, taxRate));
            return this;
        }

        public ABill With(Line line)
        {
            lines.Add(line);
            return this;
        }

        public Bill Build()
        {
            return new Bill(id, billingDate, lines, discounts, total, taxes, closed);
        }
    }
}
