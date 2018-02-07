using System;

namespace MaiDan.Billing.Domain
{
    public class TaxRate
    {
        public TaxRate(decimal rate, DateTime validityStartDate, DateTime validityEndDate)
        {
            Rate = rate;
            ValidityStartDate = validityStartDate;
            ValidityEndDate = validityEndDate;
        }

        public decimal Rate { get; }
        public DateTime ValidityStartDate { get; }
        public DateTime ValidityEndDate { get; }
    }
}
