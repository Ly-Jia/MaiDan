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

        public Order(int id, bool takeAway, string tableId, int numberOfGuests, List<Line> lines)
        {
            Id = id;
            TakeAway = takeAway;
            TableId = tableId;
            NumberOfGuests = numberOfGuests;
            Lines = lines;
        }

        public int Id { get; set; }
        public bool TakeAway { get; set; }
        public string TableId { get; set; }
        public int NumberOfGuests { get; set; }
        public List<Line> Lines { get; set; }
    }
}
