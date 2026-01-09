using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
        public DbSet<Invoice> Bills => Set<Invoice>();
        public DbSet<InvoiceDetail> BillDetails => Set<InvoiceDetail>();
        public DbSet<POD> PODs => Set<POD>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<HistPrice> HistPrices => Set<HistPrice>();
        public DbSet<Distribution> Distributions => Set<Distribution>();
        public DbSet<Planogram> Planograms => Set<Planogram>();
        public DbSet<Store> Stores => Set<Store>();
        public DbSet<VisitLog> VisitLogs => Set<VisitLog>();
        public DbSet<User> Users => Set<User>();
        public DbSet<City> Cities => Set<City>();
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}

