namespace MaiDan.Accounting.Domain
{
    public class Payment
    {
        public Payment(int id, PaymentMethod method, decimal amount)
        {
            Id = id;
            Method = method;
            Amount = amount;
        }
        
        public int Id { get; }
        public PaymentMethod Method { get; }
        public decimal Amount { get; }
    }
}
