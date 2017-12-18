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

        public override bool Equals(object obj)
        {
            if (!(obj is Price other))
                return false;
            return other.Amount == this.Amount && other.ValidityStartDate == this.ValidityStartDate &&
                   other.ValidityEndDate == this.ValidityEndDate;
        }
    }
}
