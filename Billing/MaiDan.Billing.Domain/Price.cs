using System;

namespace MaiDan.Billing.Domain
{
    public class Price
    {
        public string DishId { get; set; }
        public decimal Amount { get; set; }
        public DateTime ValidityStartDate { get; set; }
        public DateTime ValidityEndDate { get; set; }
    }
}
