namespace MaiDan.Billing.Domain
{
    public class Line
    {
        public Line(int id, decimal amount, TaxRate taxRate, decimal taxAmount)
        {
            Id = id;
            Amount = amount;
            TaxRate = taxRate;
            TaxAmount = taxAmount;
        }

        public int Id { get; }
        public decimal Amount { get; }
        public TaxRate TaxRate { get; }
        public decimal TaxAmount { get; }
    }
}
