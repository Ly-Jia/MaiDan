using MaiDan.Accounting.Dal.Entities;
using MaiDan.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using System;

namespace MaiDan.Accounting.Dal
{
    public class AccountingLogger : Logger<AccountingContext>
    {
        public AccountingLogger(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        protected override void Log(AccountingContext context,
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
