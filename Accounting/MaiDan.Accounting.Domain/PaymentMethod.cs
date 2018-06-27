namespace MaiDan.Accounting.Domain
{
    public class PaymentMethod
    {
        public PaymentMethod(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        public string Name { get; }
    }
}
