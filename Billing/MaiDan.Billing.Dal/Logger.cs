using MaiDan.Billing.Dal.Entities;
using MaiDan.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;

namespace MaiDan.Billing.Dal
{
    public class Logger : ILogger<BillingContext>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Logger(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

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
            var request = _httpContextAccessor.HttpContext.Request;
            var requestPath = request.Path.Value;
            var requestMethod = request.Method;
            var requestBody = ToJson(request.Body);

            var entity = new Log(DateTime.Now, requestPath, requestMethod, requestBody, objectType, actionType, oldJson, newJson);
            context.Logs.Add(entity);
        }

        private static string ToJson(Stream body)
        {
            body.Seek(0, SeekOrigin.Begin);
            using (var sr = new StreamReader(body))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
