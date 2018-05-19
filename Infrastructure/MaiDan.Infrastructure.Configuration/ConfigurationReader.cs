using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace MaiDan.Infrastructure
{
    public class ConfigurationReader
    {
        public static IConfiguration Configuration { get; set; }

        public static T Get<T>(string key)
        {
            CheckConfiguration();
            
            return (T)Convert.ChangeType(Configuration[key], typeof(T));
        }

        public static IEnumerable<T> GetCollection<T>(string key)
        {
            CheckConfiguration();

            var children = Configuration.GetSection(key).GetChildren();
            return children?.Select(c => (T) Convert.ChangeType(c.Value, typeof(T))).ToList();
        }     

        private static void CheckConfiguration()
        {
            if (Configuration == null)
            {
                var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                var configuration = builder.Build();
                Configuration = configuration.GetSection("App");
            }
        }
    }
}
