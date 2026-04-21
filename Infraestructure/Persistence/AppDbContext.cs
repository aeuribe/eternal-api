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
        public DbSet<Product> Products => Set<Product>();
        public DbSet<HistPrice> HistPrices => Set<HistPrice>();
        public DbSet<Distribution> Distributions => Set<Distribution>();
        public DbSet<Planogram> Planograms => Set<Planogram>();
        public DbSet<Store> Stores => Set<Store>();
        public DbSet<Assignment> Assignments => Set<Assignment>();
        public DbSet<User> Users => Set<User>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Family> Families => Set<Family>();
        public DbSet<Presentation> Presentations => Set<Presentation>();
        public DbSet<Class> Classes => Set<Class>();
        public DbSet<SalesRoute> SalesRoutes => Set<SalesRoute>();
        public DbSet<Area> Areas => Set<Area>();
        public DbSet<Region> Regions => Set<Region>();
        public DbSet<District> Districts => Set<District>();
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}

