using System;

namespace MaiDan.Billing.Domain
{
    public class TaxRate
    {
        public TaxRate(string id, Tax tax, decimal rate, DateTime validityStartDate, DateTime validityEndDate)
        {
            Id = id;
            Tax = tax;
            Rate = rate;
            ValidityStartDate = validityStartDate;
            ValidityEndDate = validityEndDate;
        }

        public string Id { get; }
        public Tax Tax { get; }
        public decimal Rate { get; }
        public DateTime ValidityStartDate { get; }
        public DateTime ValidityEndDate { get; }
    }
}
