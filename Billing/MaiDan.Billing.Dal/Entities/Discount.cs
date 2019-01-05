using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Billing.Dal.Entities
{
    [Table("Discount")]
    public class Discount
    {
        public Discount(string id, decimal rate, string applicableTaxId)
        {
            Id = id;
            Rate = rate;
            ApplicableTaxId = applicableTaxId;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public decimal Rate { get; set; }

        public string ApplicableTaxId { get; set; }
    }
}
