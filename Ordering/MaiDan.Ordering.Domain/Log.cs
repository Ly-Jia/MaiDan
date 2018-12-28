using System;

namespace MaiDan.Ordering.Domain
{
    public class Log
    {
        public Log(DateTime date, string objectType, string actionType, string oldValue, string newValue)
        {
            Date = date;
            ObjectType = objectType;
            ActionType = actionType;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public DateTime Date { get; }
        public string ObjectType { get; }
        public string ActionType { get; }
        public string OldValue { get; }
        public string NewValue { get; }
    }
}
