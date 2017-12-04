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

        public Order(int id, string tableId, int numberOfGuests, List<Line> lines)
        {
            Id = id;
            TableId = tableId;
            NumberOfGuests = numberOfGuests;
            Lines = lines;
        }

        public int Id { get; set; }
        public string TableId { get; set; }
        public int NumberOfGuests { get; set; }
        public List<Line> Lines { get; set; }
    }
}
