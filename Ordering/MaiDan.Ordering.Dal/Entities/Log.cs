using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Ordering.Dal.Entities
{
    [Table("OrderLog")]
    public class Log
    {
        public Log()
        {
        }

        public Log(DateTime date, string objectType, string actionType, string oldValue, string newValue)
        {
            Date = date;
            ObjectType = objectType;
            ActionType = actionType;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ObjectType { get; set; }
        public string ActionType { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
