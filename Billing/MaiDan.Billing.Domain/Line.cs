namespace MaiDan.Billing.Domain
{
    public class Line
    {
        public Line(int id, decimal amount, Tax tax, decimal taxAmount)
        {
            Id = id;
            Amount = amount;
            Tax = tax;
            TaxAmount = taxAmount;
        }

        public int Id { get; }
        public decimal Amount { get; }
        public Tax Tax { get; }
        public decimal TaxAmount { get; }
    }
}
