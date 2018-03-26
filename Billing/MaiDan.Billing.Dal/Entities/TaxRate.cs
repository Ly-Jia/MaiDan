using System;
using Dapper.Contrib.Extensions;

namespace MaiDan.Billing.Dal.Entities
{
    [Table("Tax")]
    public class TaxRate
    {
        public TaxRate(string taxId, int index, DateTime validFrom, DateTime validTo, decimal rate)
        {
            Id = $"{taxId}-{index}";
            TaxId = taxId;
            Index = index;
            ValidityStartDate = validFrom;
            ValidityEndDate = validTo;
            Rate = rate;
        }

        [ExplicitKey]
        public string Id { get; set; }
        public string TaxId { get; set; }
        public int Index { get; set; }
        public DateTime ValidityStartDate { get; set; }
        public DateTime ValidityEndDate { get; set; }
        public decimal Rate { get; set; }
    }
}
