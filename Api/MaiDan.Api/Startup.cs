using MaiDan.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MaiDan.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add service and create Policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            // Add framework services.
            services.AddMvc();

            var database = new SqliteDatabase("MaiDan.sqlite");
            services.AddSingleton<IDatabase, SqliteDatabase>(svcs => database);
            services.AddSingleton<IRepository<Ordering.Domain.Dish>, Ordering.Dal.Repositories.Menu>(svcs => new Ordering.Dal.Repositories.Menu(database));
            services.AddSingleton<IRepository<Billing.Domain.Dish>, Billing.Dal.Repositories.Menu>(svcs => new Billing.Dal.Repositories.Menu(database));
            services.AddSingleton<IRepository<Ordering.Domain.Order>, Ordering.Dal.Repositories.OrderBook>(svcs => new Ordering.Dal.Repositories.OrderBook(database));
            services.AddSingleton<IRepository<Ordering.Domain.Table>, Ordering.Dal.Repositories.Room>(svcs => new Ordering.Dal.Repositories.Room(database));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors("CorsPolicy");

            app.UseMvc();
        }
        
    }
}
