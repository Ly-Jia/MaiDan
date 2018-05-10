namespace MaiDan.Api.DataContracts.Responses
{
    public class Discount
    {
        public Discount(string name, decimal rate, decimal amount)
        {
            Name = name;
            Rate = rate;
            Amount = amount;
        }
        public string Name { get; set; }

        public decimal Rate { get; set; }

        public decimal Amount { get; set; }
    }
}
