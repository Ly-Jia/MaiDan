using Microsoft.EntityFrameworkCore;

namespace MaiDan.Infrastructure.Database
{
    public interface ILogger<TContext> where TContext : DbContext
    {
        void Log(TContext context, string objectType, string actionType, object value);

        void Log(TContext context, string objectType, string actionType, object oldValue, object newValue);
    }
}
