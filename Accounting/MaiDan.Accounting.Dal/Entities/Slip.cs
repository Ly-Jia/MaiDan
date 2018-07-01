using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Accounting.Dal.Entities
{
    [Table("Slip")]
    public class Slip
    {
        public Slip()
        { }

        public Slip(int id, DateTime paymentDate, List<Payment> payments)
        {
            Id = id;
            PaymentDate = paymentDate;
            Payments = payments;
        }

        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
