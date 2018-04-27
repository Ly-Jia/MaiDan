using MaiDan.Billing.Dal;
using MaiDan.Ordering.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace MaiDan.Api
{
    public static class DataSeeder
    {
        /// <summary>
        /// Creates the database or migrates it to the latest version.
        /// </summary>
        public static void SeedData(this IApplicationBuilder app)
        {
            var orderingContext = new OrderingContext();
            orderingContext.Database.Migrate();

            var billingContext = new BillingContext();
            billingContext.Database.Migrate();
        }
    }
}
