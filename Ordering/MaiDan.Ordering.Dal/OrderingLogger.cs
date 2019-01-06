using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Dal.Entities;
using Microsoft.AspNetCore.Http;
using System;

namespace MaiDan.Ordering.Dal
{
    public class OrderingLogger : Logger<OrderingContext>
    {
        public OrderingLogger(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        protected override void Log(OrderingContext context,
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
