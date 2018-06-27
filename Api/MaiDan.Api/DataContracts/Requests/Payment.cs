namespace MaiDan.Api.DataContracts.Requests
{
    public class Payment
    {
        public int Id { get; set; }

        public string PaymentMethodId { get; set; }

        public decimal Amount { get; set; }
    }
}
