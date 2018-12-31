using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Accounting.Dal.Entities
{
    [Table("Day")]
    public class Day
    {
        public Day(DateTime date, bool closed)
        {
            Date = date;
            Closed = closed;
        }

        [Key]
        public DateTime Date { get; set; }
        public bool Closed { get; set; }
    }
}
