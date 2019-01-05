using MaiDan.Accounting.Dal.Entities;
using MaiDan.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using System;

namespace MaiDan.Accounting.Dal
{
    public class Logger : Logger<AccountingContext>
    {
        public Logger(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        protected override void Log(AccountingContext context,
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
