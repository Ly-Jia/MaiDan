using System.Collections.Generic;

namespace MaiDan.Accounting.Domain
{
    public class Slip 
    {
        public Slip(int id, IList<Payment> payments)
        {
            Id = id;
            Payments = payments;
        }

        public Slip(int id) : this(id, new List<Payment>())
        { }

        public int Id { get; }
        public IList<Payment> Payments { get; }
    }
}
