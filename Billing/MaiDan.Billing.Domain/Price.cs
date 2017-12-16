using System;

namespace MaiDan.Billing.Domain
{
    public class Price
    {
        public Price(decimal amount, DateTime validityStartDate, DateTime validityEndDate)
        {
            Amount = amount;
            ValidityStartDate = validityStartDate;
            ValidityEndDate = validityEndDate;
        }
        
        public decimal Amount { get; }
        public DateTime ValidityStartDate { get; }
        public DateTime ValidityEndDate { get; }
    }
}
