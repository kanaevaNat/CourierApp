using Microsoft.EntityFrameworkCore;
using CourierAdminService.Models;
namespace CourierAdminService.Context
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Status>? Statuses { get; set; }
        public DbSet<Courier>? Couriers { get; set; }
        public DbSet<Order>? Orders { get; set; }

        public DbSet<Operator> Operators { get; set; }

        public DataBaseContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=courier_db;Username=postgres;Password=yfgjktjy1813");
        }
    }
}
