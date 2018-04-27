using MaiDan.Billing.Dal;
using MaiDan.Ordering.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MaiDan.Api
{
    public static class DataSeeder
    {
        /// <summary>
        /// Creates the database or migrates it to the latest version.
        /// </summary>
        public static void SeedData(this IApplicationBuilder app)
        {
            var orderingContext = app.ApplicationServices.GetService<OrderingContext>();
            orderingContext.Database.Migrate();

            var billingContext = app.ApplicationServices.GetService<BillingContext>();
            billingContext.Database.Migrate();
        }
    }
}
