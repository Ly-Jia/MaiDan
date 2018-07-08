using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Billing.Dal.Entities
{
    [Table("Bill")]
    public class Bill
    {
        public Bill()
        {
        }

        public Bill(int id, DateTime billingDate, decimal total, List<Line> lines, List<BillDiscount> discounts, List<BillTax> taxes, bool closed)
        {
            Id = id;
            BillingDate = billingDate;
            Closed = closed;
            Total = total;
            Lines = lines;
            Discounts = discounts;
            Taxes = taxes;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public DateTime BillingDate { get; set; }

        public decimal Total { get; set; }

        public List<Line> Lines { get; set; }

        public List<BillDiscount> Discounts { get; set; }

        public List<BillTax> Taxes { get; set; }

        public bool Closed { get; set; }
    }
}
