using System.Data.Common;

namespace MaiDan.Infrastructure.Database
{
    public interface IDatabase
    {
        DbConnection CreateConnection();
    }
}
