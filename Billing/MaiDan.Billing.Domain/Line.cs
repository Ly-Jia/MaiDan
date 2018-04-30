namespace MaiDan.Billing.Domain
{
    public class Line
    {
        public Line(int id, decimal amount, TaxRate taxRate)
        {
            Id = id;
            Amount = amount;
            TaxRate = taxRate;
        }

        public int Id { get; }
        public decimal Amount { get; }
        public TaxRate TaxRate { get; }
    }
}
