using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Accounting.Dal.Entities
{
    [Table("Slip")]
    public class Slip
    {
        public Slip()
        { }

        public Slip(int id, List<Payment> payments)
        {
            Id = id;
            Payments = payments;
        }

        public int Id { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
