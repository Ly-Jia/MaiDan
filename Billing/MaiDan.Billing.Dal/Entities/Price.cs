using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Billing.Dal.Entities
{
    [Table("DishPrice")]
    public class Price
    {
        public Price(string dishId, DateTime validityStartDate, DateTime validityEndDate, decimal amount)
        {
            DishId = dishId;
            ValidityStartDate = validityStartDate;
            ValidityEndDate = validityEndDate;
            Amount = amount;
        }

        public string DishId { get; set; }
        public DateTime ValidityStartDate { get; set; }
        public DateTime ValidityEndDate { get; set; }
        public decimal Amount { get; set; }
    }
}
