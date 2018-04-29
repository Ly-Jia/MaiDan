using System.IO;
using System.Reflection;

namespace MaiDan.Infrastructure.Database
{
    public static class DbConfiguration
    {
        public static readonly string ConnectionString = GetConnectionString();

        private static string GetConnectionString()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dbPath = Path.Combine(path, "MaiDan.sqlite");
            return $"Data Source={dbPath}";
        }
    }
}
