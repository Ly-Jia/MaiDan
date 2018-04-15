using Dapper.Contrib.Extensions;

namespace MaiDan.Billing.Dal.Entities
{
    [Table("BillTax")]
    public class BillTax
    {

        /// <summary>
        /// Constructor used by Dapper only
        /// </summary>
        public BillTax()
        {
        }

        public BillTax(int billId, int index, TaxRate taxRate, decimal amount)
        {
            Id = $"{billId}-{index}";
            BillId = billId;
            Index = index;
            TaxRate = taxRate;
            Amount = amount;
        }

        [ExplicitKey]
        public string Id { get; set; }

        public int BillId { get; set; }

        public int Index { get; set; }

        public TaxRate TaxRate { get; set; }

        public decimal Amount { get; set; }
    }
}
