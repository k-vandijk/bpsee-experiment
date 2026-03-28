using BPSEE.Experiment.Domain.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BPSEE.Experiment.Migrations.Products;

public class ProductsDbContextFactory : IDesignTimeDbContextFactory<ProductsDbContext>
{
    public ProductsDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING_PRODUCTS")!;

        var optionsBuilder = new DbContextOptionsBuilder<ProductsDbContext>();
        optionsBuilder.UseSqlServer(connectionString, sql =>
            sql.MigrationsAssembly("BPSEE.Experiment.Migrations.Products"));

        return new ProductsDbContext(optionsBuilder.Options);
    }
}
