using System.Collections.Generic;
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

        public Bill(int id, decimal total, List<Line> lines, List<BillTax> taxes)
        {
            Id = id;
            Total = total;
            Lines = lines;
            Taxes = taxes;
        }
        
        [ExplicitKey]
        public int Id { get; set; }
        
        public decimal Total { get; set; }
        
        public List<Line> Lines { get; set; }
        
        public List<BillTax> Taxes { get; set; }
    }
}
