using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Dal.Entities;
using Microsoft.AspNetCore.Http;
using System;

namespace MaiDan.Ordering.Dal
{
    public class Logger : Logger<OrderingContext>
    {
        public Logger(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        protected override void Log(OrderingContext context,
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
