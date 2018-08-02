namespace MaiDan.Api.DataContracts.Responses
{
    public class PaymentMethod
    {
        public PaymentMethod()
        {
        }

        public PaymentMethod(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}