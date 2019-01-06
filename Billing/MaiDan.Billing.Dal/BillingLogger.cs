using MaiDan.Billing.Dal.Entities;
using MaiDan.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using System;

namespace MaiDan.Billing.Dal
{
    public class BillingLogger : Logger<BillingContext>
    {
        public BillingLogger(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        protected override void Log(BillingContext context,
            DateTime date,
            Guid requestId,
            string requestPath,
            string requestMethod,
            string requestBody,
            string objectType,
            string actionType,
            string oldValue,
            string newValue)
        {
            var entity = new Log(date, requestId, requestPath, requestMethod, requestBody, objectType, actionType, oldValue, newValue);
            context.Logs.Add(entity);
        }
    }
}
