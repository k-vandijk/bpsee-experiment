using BPSEE.Experiment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BPSEE.Experiment.Domain.Contexts;

public class OrdersDbContext : DbContext
{
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderLine> OrderLines => Set<OrderLine>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // These entities are not part of this bounded context
        modelBuilder.Ignore<User>();
        modelBuilder.Ignore<Product>();

        modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2);
        modelBuilder.Entity<OrderLine>().Property(ol => ol.UnitPrice).HasPrecision(18, 2);
    }
}