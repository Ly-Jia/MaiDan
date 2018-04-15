using System;
using Dapper.Contrib.Extensions;

namespace MaiDan.Billing.Dal.Entities
{
    [Table("TaxRate")]
    public class TaxRate
    {
        /// <summary>
        /// Constructor used by Dapper only
        /// </summary>
        public TaxRate()
        {        
        }

        public TaxRate(string id, string taxId, DateTime validFrom, DateTime validTo, decimal rate)
        {
            Id = id;
            TaxId = taxId;
            ValidityStartDate = validFrom;
            ValidityEndDate = validTo;
            Rate = rate;
        }

        public TaxRate(Domain.TaxRate taxRate)
        {
            Id = taxRate.Id;
            TaxId = taxRate.Tax.Id;
            ValidityStartDate = taxRate.ValidityStartDate;
            ValidityEndDate = taxRate.ValidityEndDate;
            Rate = taxRate.Rate;
        }

        [ExplicitKey]
        public string Id { get; set; }
        public string TaxId { get; set; }
        public DateTime ValidityStartDate { get; set; }
        public DateTime ValidityEndDate { get; set; }
        public decimal Rate { get; set; }
    }
}
