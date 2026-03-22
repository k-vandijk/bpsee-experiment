using BPSEE.Experiment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BPSEE.Experiment.Domain.Contexts;

public class MonolithicDbContext : DbContext
{
    public MonolithicDbContext(DbContextOptions<MonolithicDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderLine> OrderLines => Set<OrderLine>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2);
        modelBuilder.Entity<OrderLine>().Property(ol => ol.UnitPrice).HasPrecision(18, 2);
        modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
    }
}