namespace MaiDan.Billing.Domain
{
    public class BillTax
    {
        public BillTax(int id, TaxRate taxRate, decimal amount)
        {
            Id = id;
            TaxRate = taxRate;
            Amount = amount;
        }

        public int Id { get; }
        public TaxRate TaxRate { get; }
        public decimal Amount { get; }
    }
}
