using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Ordering.Dal.Entities
{
    [Table("Order")]
    public class Order 
    {
        public Order()
        {
        }

        public Order(int id, bool takeAway, Table table, int numberOfGuests, List<Line> lines, bool closed)
        {
            Id = id;
            TakeAway = takeAway;
            Table = table;
            NumberOfGuests = numberOfGuests;
            Lines = lines;
            Closed = closed;
        }

        public int Id { get; set; }
        public bool TakeAway { get; set; }
        public Table Table { get; set; }
        public int NumberOfGuests { get; set; }
        public List<Line> Lines { get; set; }
        public bool Closed { get; set; }
    }
}
