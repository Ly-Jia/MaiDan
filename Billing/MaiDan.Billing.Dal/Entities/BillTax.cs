using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Billing.Dal.Entities
{
    [Table("BillTax")]
    public class BillTax
    {
        public BillTax()
        {
        }

        public BillTax(int billId, int index, TaxRate taxRate, decimal amount)
        {
            BillId = billId;
            Index = index;
            TaxRate = taxRate;
            Amount = amount;
        }

        public int BillId { get; set; }

        public int Index { get; set; }

        [Required]
        public TaxRate TaxRate { get; set; }

        public decimal Amount { get; set; }
    }
}
