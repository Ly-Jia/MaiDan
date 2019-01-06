using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Billing.Dal.Entities
{
    [Table("BillLog")]
    public class Log
    {
        public Log(DateTime date,
            Guid requestId,
            string requestPath,
            string requestMethod,
            string requestBody,
            string objectType,
            string actionType,
            string oldValue,
            string newValue)
        {
            Date = date;
            RequestId = requestId;
            RequestPath = requestPath;
            RequestMethod = requestMethod;
            RequestBody = requestBody;
            ObjectType = objectType;
            ActionType = actionType;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Guid RequestId { get; set; }
        public string RequestPath { get; set; }
        public string RequestMethod { get; set; }
        public string RequestBody { get; set; }
        public string ObjectType { get; set; }
        public string ActionType { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
