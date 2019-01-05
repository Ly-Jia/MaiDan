using MaiDan.Billing.Dal.Entities;
using MaiDan.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using System;

namespace MaiDan.Billing.Dal
{
    public class Logger : Logger<BillingContext>
    {
        public Logger(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        protected override void Log(BillingContext context,
            DateTime date,
            string requestPath,
            string requestMethod,
            string requestBody,
            string objectType,
            string actionType,
            string oldValue,
            string newValue)
        {
            var entity = new Log(date, requestPath, requestMethod, requestBody, objectType, actionType, oldValue, newValue);
            context.Logs.Add(entity);
        }
    }
}
