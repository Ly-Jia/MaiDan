using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Accounting.Dal.Entities
{
    [Table("SlipPayment")]
    public class Payment
    {
        public Payment()
        { }

        public Payment(int slipId, int index, PaymentMethod paymentMethod, decimal amount)
        {
            SlipId = slipId;
            Index = index;
            PaymentMethod = paymentMethod;
            Amount = amount;
        }

        public int SlipId { get; set; }
        public int Index { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }
}
