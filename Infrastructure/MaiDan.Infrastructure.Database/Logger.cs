using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.IO;

namespace MaiDan.Infrastructure.Database
{
    public abstract class Logger<TContext> : ILogger<TContext> where TContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Logger(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Log(TContext context, string objectType, string actionType, object value)
        {
            var newJson = JsonConvert.SerializeObject(value);
            Log(context, objectType, actionType, null, newJson);
        }

        public void Log(TContext context, string objectType, string actionType, object oldValue, object newValue)
        {
            var oldJson = JsonConvert.SerializeObject(oldValue);
            var newJson = JsonConvert.SerializeObject(newValue);
            Log(context, objectType, actionType, oldJson, newJson);
        }

        private void Log(TContext context, string objectType, string actionType, string oldValue, string newValue)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var request = httpContext.Request;
            var requestId = GetRequestId(httpContext);
            var requestPath = request.Path.Value;
            var requestMethod = request.Method;
            var requestBody = ToJson(request.Body);

            Log(context, DateTime.Now, requestId, requestPath, requestMethod, requestBody, objectType, actionType, oldValue, newValue);
        }

        private static Guid GetRequestId(HttpContext httpContext)
        {
            if (!httpContext.Items.TryGetValue("RequestId", out var id))
            {
                id = Guid.NewGuid();
                httpContext.Items.Add("RequestId", id);
            }

            return (Guid)id;
        }

        private static string ToJson(Stream body)
        {
            body.Seek(0, SeekOrigin.Begin);
            var sr = new StreamReader(body);
            return sr.ReadToEnd();
        }

        protected abstract void Log(TContext context,
            DateTime date,
            Guid requestId,
            string requestPath,
            string requestMethod,
            string requestBody,
            string objectType,
            string actionType,
            string oldValue,
            string newValue);
    }
}
