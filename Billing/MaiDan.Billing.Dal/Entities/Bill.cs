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

        public Bill(int id, decimal total, List<Line> lines, List<BillTax> taxes)
        {
            Id = id;
            Total = total;
            Lines = lines;
            Taxes = taxes;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        
        public decimal Total { get; set; }
        
        public List<Line> Lines { get; set; }
        
        public List<BillTax> Taxes { get; set; }
    }
}
