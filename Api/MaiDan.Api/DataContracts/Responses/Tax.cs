namespace MaiDan.Api.DataContracts.Responses
{
    public class Tax
    {
        public Tax(decimal rate, decimal amount)
        {
            Rate = rate;
            Amount = amount;
        }

        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
