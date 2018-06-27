namespace MaiDan.Api.DataContracts.Responses
{
    public class Payment
    {
        public Payment()
        { }

        public Payment(int id, string paymentMethodId, decimal amount)
        {
            Id = id;
            PaymentMethodId = paymentMethodId;
            Amount = amount;
        }

        public int Id { get; set; }
        public string PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
    }
}
