using MaiDan.Api.Services;
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

            // Ordering
            services.AddSingleton<IRepository<Ordering.Domain.Dish>, Ordering.Dal.Repositories.Menu>(svcs => new Ordering.Dal.Repositories.Menu());
            services.AddSingleton<IRepository<Ordering.Domain.Order>, Ordering.Dal.Repositories.OrderBook>(svcs => new Ordering.Dal.Repositories.OrderBook());
            services.AddSingleton<IRepository<Ordering.Domain.Table>, Ordering.Dal.Repositories.Room>(svcs => new Ordering.Dal.Repositories.Room());

            // Billing
            var taxConfiguration = new Billing.Dal.Repositories.TaxConfiguration();
            var taxRateList = new Billing.Dal.Repositories.TaxRateList(taxConfiguration);
            var billingMenu = new Billing.Dal.Repositories.Menu();
            var billBook = new Billing.Dal.Repositories.BillBook(taxRateList);
            var cashRegister = new CashRegister(billingMenu, billBook, taxConfiguration);
            services.AddSingleton<IRepository<Billing.Domain.Dish>, Billing.Dal.Repositories.Menu>(svcs => billingMenu);
            services.AddSingleton<IRepository<Billing.Domain.Bill>, Billing.Dal.Repositories.BillBook>(svcs => billBook);
            services.AddSingleton<ICashRegister>(svcs => cashRegister);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors("CorsPolicy");

            app.UseMvc();
            app.SeedData();
        }
    }
}
