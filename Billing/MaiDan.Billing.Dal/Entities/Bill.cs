using System.Collections.Generic;
using System.Linq;
using Dapper.Contrib.Extensions;

namespace MaiDan.Billing.Dal.Entities
{
    [Table("Bill")]
    public class Bill
    {
        /// <summary>
        /// Constructor used by Dapper
        /// </summary>
        public Bill()
        {
        }

        public Bill(Domain.Bill bill)
        {
            Id = bill.Id;
            Total = bill.Total;
            Lines = bill.Lines.Select(l => new Line(bill.Id, l.Id, l.Amount)).ToList();
        }

        public int Id { get; set; }
        
        public decimal Total { get; set; }

        public List<Line> Lines { get; set; }
    }
}
