using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Billing.Dal.Entities
{
    [Table("BillTax")]
    public class BillTax
    {
        public BillTax(int billId, string taxRateId, decimal amount)
        {
            BillId = billId;
            TaxRateId = taxRateId;
            Amount = amount;
        }

        public int BillId { get; set; }
        
        public string TaxRateId { get; set; }

        public decimal Amount { get; set; }
    }
}
