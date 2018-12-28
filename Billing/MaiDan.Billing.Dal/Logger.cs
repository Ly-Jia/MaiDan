using MaiDan.Infrastructure.Database;
using MaiDan.Billing.Dal.Entities;
using Newtonsoft.Json;
using System;

namespace MaiDan.Billing.Dal
{
    public class Logger : ILogger<BillingContext>
    {
        public void Log(BillingContext context, string objectType, string actionType, object value)
        {
            var newJson = JsonConvert.SerializeObject(value);
            Log(context, objectType, actionType, null, newJson);
        }

        public void Log(BillingContext context, string objectType, string actionType, object oldValue, object newValue)
        {
            var oldJson = JsonConvert.SerializeObject(oldValue);
            var newJson = JsonConvert.SerializeObject(newValue);
            Log(context, objectType, actionType, oldJson, newJson);
        }

        private void Log(BillingContext context, string objectType, string actionType, string oldJson, string newJson)
        {
            var entity = new Log(DateTime.Now, objectType, actionType, oldJson, newJson);
            context.Logs.Add(entity);
        }
    }
}
