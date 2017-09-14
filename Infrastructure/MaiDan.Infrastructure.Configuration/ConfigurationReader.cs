﻿using System;
using Microsoft.Extensions.Configuration;

namespace MaiDan.Infrastructure.Configuration
{
    public class ConfigurationReader
    {
        public static IConfiguration Configuration { get; set; }

        public static T Get<T>(string key)
        {
            if (Configuration == null)
            {
                var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                var configuration = builder.Build();
                Configuration = configuration.GetSection("AppSettings");
            }

            return (T)Convert.ChangeType(Configuration[key], typeof(T));
        }
    }
}
