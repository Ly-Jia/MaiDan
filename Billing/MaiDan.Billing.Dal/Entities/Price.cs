using System;
using Dapper.Contrib.Extensions;

namespace MaiDan.Billing.Dal.Entities
{
    [Table("DishPrice")]
    public class Price
    {
        /// <summary>
        /// Constructor used by Dapper
        /// </summary>
        public Price()
        {
        }

        public Price(string dishId, DateTime validFrom, DateTime validTo, decimal amount)
        {
            DishId = dishId;
            ValidityStartDate = validFrom;
            ValidityEndDate = validTo;
            Amount = amount;
        }
        public string DishId { get; set; }
        public DateTime ValidityStartDate { get; set; }
        public DateTime ValidityEndDate { get; set; }

        //FIXME: Remove this hack that avoid Dapper failing to convert from database decimal to c# decimal (tries to convert to Int64)
        public object AmountInternal { get; set; }

        public decimal Amount
        {
            get => Convert.ToDecimal(AmountInternal);
            set => AmountInternal = value;
        }
    }
}
