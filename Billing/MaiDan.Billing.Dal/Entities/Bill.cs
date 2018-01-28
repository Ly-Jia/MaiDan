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
        }

        public int Id { get; set; }
        
        public decimal Total { get; set; }
    }
}
