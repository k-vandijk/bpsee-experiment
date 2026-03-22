using BPSEE.Experiment.Domain.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BPSEE.Experiment.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        var monolithicConnectionString = configuration.GetConnectionString("MonolithicDb");
        if (monolithicConnectionString != null)
        {
            services.AddDbContext<MonolithicDbContext>(options =>
                options.UseSqlServer(
                    monolithicConnectionString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("BPSEE.Experiment.Migrations.Monolithic");
                        sqlOptions.EnableRetryOnFailure();
                    }));
        }

        var ordersConnectionString = configuration.GetConnectionString("OrdersDb");
        if (ordersConnectionString != null)
        {
            services.AddDbContext<OrdersDbContext>(options =>
                options.UseSqlServer(
                    ordersConnectionString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("BPSEE.Experiment.Migrations.Orders");
                        sqlOptions.EnableRetryOnFailure();
                    }));
        }

        var productsConnectionString = configuration.GetConnectionString("ProductsDb");
        if (productsConnectionString != null)
        {
            services.AddDbContext<ProductsDbContext>(options =>
                options.UseSqlServer(
                    productsConnectionString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("BPSEE.Experiment.Migrations.Products");
                        sqlOptions.EnableRetryOnFailure();
                    }));
        }

        var usersConnectionString = configuration.GetConnectionString("UsersDb");
        if (usersConnectionString != null)
        {
            services.AddDbContext<UsersDbContext>(options =>
                options.UseSqlServer(
                    usersConnectionString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("BPSEE.Experiment.Migrations.Users");
                        sqlOptions.EnableRetryOnFailure();
                    }));
        }

        return services;
    }
}