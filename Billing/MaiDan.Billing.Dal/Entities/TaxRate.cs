using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Billing.Dal.Entities
{
    [Table("TaxRate")]
    public class TaxRate
    {
        public TaxRate(string id, string taxId, DateTime validityStartDate, DateTime validityEndDate, decimal rate)
        {
            Id = id;
            TaxId = taxId;
            ValidityStartDate = validityStartDate;
            ValidityEndDate = validityEndDate;
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

        public string Id { get; set; }
        [Required]
        public string TaxId { get; set; }
        public DateTime ValidityStartDate { get; set; }
        public DateTime ValidityEndDate { get; set; }
        public decimal Rate { get; set; }
    }
}
