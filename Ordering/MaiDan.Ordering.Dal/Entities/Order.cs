using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace MaiDan.Ordering.Dal.Entities
{
    [Table("Order")]
    public class Order 
    {
        /// <summary>
        /// Constructor used by Dapper
        /// </summary>
        public Order()
        {
        }

        public Order(int id, List<Line> lines)
        {
            Id = id;
            Lines = lines;
        }

        public int Id { get; set; }
        public List<Line> Lines { get; set; }
    }
}
