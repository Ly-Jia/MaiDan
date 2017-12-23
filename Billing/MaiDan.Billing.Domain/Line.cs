namespace MaiDan.Billing.Domain
{
    public class Line
    {
        public Line(int id, decimal amount)
        {
            Id = id;
            Amount = amount;
        }

        public int Id { get; }
        public decimal Amount { get; }
    }
}
