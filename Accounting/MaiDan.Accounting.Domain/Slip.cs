using System;
using System.Collections.Generic;

namespace MaiDan.Accounting.Domain
{
    public class Slip 
    {
        public Slip(int id, DateTime paymentDate, IList<Payment> payments)
        {
            Id = id;
            PaymentDate = paymentDate;
            Payments = payments;
        }

        public Slip(int id) : this(id, DateTime.Now, new List<Payment>())
        { }

        public int Id { get; }
        public DateTime PaymentDate { get; }
        public IList<Payment> Payments { get; }
    }
}
