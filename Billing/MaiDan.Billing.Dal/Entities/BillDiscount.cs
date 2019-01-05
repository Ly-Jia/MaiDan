using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Billing.Dal.Entities
{
    [Table("BillDiscount")]
    public class BillDiscount
    {
        public BillDiscount(int billId, string discountId, decimal amount)
        {
            BillId = billId;
            DiscountId = discountId;
            Amount = amount;
        }

        public int BillId { get; set; }
        
        public string DiscountId { get; set; }

        public decimal Amount { get; set; }
    }
}