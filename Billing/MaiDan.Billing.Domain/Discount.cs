namespace MaiDan.Billing.Domain
{
    public class Discount
    {
        public Discount(string id, decimal rate, Tax applicableTax)
        {
            Id = id;
            Rate = rate;
            ApplicableTax = applicableTax;
        }

        public string Id { get; private set; }

        public decimal Rate { get; private set; }

        public Tax ApplicableTax { get; private set; }
    }
}