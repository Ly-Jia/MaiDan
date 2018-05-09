using MaiDan.Billing.Dal.Entities;
using MaiDan.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MaiDan.Billing.Dal
{
    public class BillingContext : DbContext
    {
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<BillTax> BillTaxes { get; set; }
        public DbSet<TaxRate> TaxRates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(DbConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Line>()
                .HasKey(l => new { l.BillId, l.Index });
            modelBuilder.Entity<BillTax>()
                .HasKey(t => new { t.BillId, t.TaxRateId });
            modelBuilder.Entity<Price>()
                .HasKey(p => new { p.DishId, p.ValidityStartDate });
        }
    }
}
