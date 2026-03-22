using BPSEE.Experiment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BPSEE.Experiment.Domain.Contexts;

public class ProductsDbContext : DbContext
{
    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {
    }
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
    }
}