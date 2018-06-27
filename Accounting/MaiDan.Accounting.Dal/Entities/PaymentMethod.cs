using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Accounting.Dal.Entities
{
    [Table("PaymentMethod")]
    public class PaymentMethod
    {
        public PaymentMethod()
        { }

        public PaymentMethod(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
