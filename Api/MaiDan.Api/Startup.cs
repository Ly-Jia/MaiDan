using MaiDan.Api.Services;
using MaiDan.Billing.Dal;
using MaiDan.Infrastructure;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Dal;
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

            services.AddDbContext<OrderingContext>();
            services.AddDbContext<BillingContext>();

            // Ordering
            services.AddSingleton<IRepository<Ordering.Domain.Dish>, Ordering.Dal.Repositories.Menu>();
            services.AddSingleton<IRepository<Ordering.Domain.Order>, Ordering.Dal.Repositories.OrderBook>();
            services.AddSingleton<IRepository<Ordering.Domain.Table>, Ordering.Dal.Repositories.Room>();

            // Billing
            services.AddSingleton<IRepository<Billing.Domain.Tax>, Billing.Dal.Repositories.TaxConfiguration>();
            services.AddSingleton<IRepository<Billing.Domain.TaxRate>, Billing.Dal.Repositories.TaxRateList>();
            services.AddSingleton<IRepository<Billing.Domain.Discount>, Billing.Dal.Repositories.DiscountList>();
            services.AddSingleton<IRepository<Billing.Domain.Dish>, Billing.Dal.Repositories.Menu>();
            services.AddSingleton<IRepository<Billing.Domain.Bill>, Billing.Dal.Repositories.BillBook>();

            var printerName = ConfigurationReader.Get<string>("PrinterName");
            var printer = new Printer(printerName);
            services.AddSingleton(svcs => printer);

            services.AddSingleton<ICashRegister, CashRegister>();
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
