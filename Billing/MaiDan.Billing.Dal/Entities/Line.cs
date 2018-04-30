using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Billing.Dal.Entities
{
    [Table("BillLine")]
    public class Line
    {
        public Line()
        {
        }

        public Line(int billId, int index, decimal amount, TaxRate taxRate)
        {
            BillId = billId;
            Index = index;
            Amount = amount;
            TaxRate = taxRate;
        }

        public int BillId { get; set; }

        public int Index { get; set; }
        
        public decimal Amount { get; set; }

        [Required]
        public TaxRate TaxRate { get; set; }
    }
}
