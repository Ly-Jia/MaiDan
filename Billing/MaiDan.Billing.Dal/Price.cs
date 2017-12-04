using System;

namespace MaiDan.Billing.Dal
{
    public class Price
    {
        public string DishId { get; set; }
        public DateTime ValidityStartDate { get; set; }
        public DateTime ValidityEndDate { get; set; }
        public decimal Amount { get; set; }
    }
}
