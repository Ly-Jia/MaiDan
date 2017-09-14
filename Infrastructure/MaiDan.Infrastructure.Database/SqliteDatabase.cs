using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace MaiDan.Infrastructure.Database
{
    public class SqliteDatabase : IDatabase
    {
        private string datasource;

        public SqliteDatabase(string databaseName)
        {
            datasource = $"Data Source = {databaseName}";
        }

        public DbConnection CreateConnection()
        {
            return new SqliteConnection(datasource);
        }
    }
}
