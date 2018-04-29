using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace MaiDan.Ordering.Dal
{
    public class OrderingContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Table> Tables { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(DbConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Line>()
                .HasKey(l => new { l.OrderId, l.Index });
        }
    }
}
