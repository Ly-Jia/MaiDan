using Dapper.Contrib.Extensions;

namespace MaiDan.Billing.Dal.Entities
{
    [Table("BillLine")]
    public class Line
    {
        /// <summary>
        /// Constructor used by Dapper only
        /// </summary>
        public Line()
        {
        }

        public Line(int billId, int index, decimal amount)
        {
            Id = $"{billId}-{index}";
            BillId = billId;
            Index = index;
            Amount = amount;
        }

        [ExplicitKey]
        public string Id { get; set; }

        public int BillId { get; set; }

        public int Index { get; set; }
        
        public decimal Amount { get; set; }
    }
}
