using System;

namespace MaiDan.Api.DataContracts.Responses
{
    public class Price
    {
        public Price(decimal amount, DateTime validityStartDate, DateTime validityEndDate)
        {
            Amount = amount;
            ValidityStartDate = validityStartDate;
            ValidityEndDate = validityEndDate;
        }

        public decimal Amount { get; set; }

        public DateTime ValidityStartDate { get; set; }

        public DateTime ValidityEndDate { get; set; }
    }
}