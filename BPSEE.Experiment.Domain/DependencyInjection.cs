using BPSEE.Experiment.Domain.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BPSEE.Experiment.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        var ordersEnvironmentVariable = Environment.GetEnvironmentVariable("CONNECTION_STRING_ORDERS");
        if (!string.IsNullOrWhiteSpace(ordersEnvironmentVariable))
        {
            services.AddDbContext<OrdersDbContext>(options =>
                options.UseSqlServer(
                    ordersEnvironmentVariable,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("BPSEE.Experiment.Migrations.Orders");
                        sqlOptions.EnableRetryOnFailure();
                    }));
        }

        var productsEnvironmentVariable = Environment.GetEnvironmentVariable("CONNECTION_STRING_PRODUCTS");
        if (!string.IsNullOrWhiteSpace(productsEnvironmentVariable))
        {
            services.AddDbContext<ProductsDbContext>(options =>
                options.UseSqlServer(
                    productsEnvironmentVariable,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("BPSEE.Experiment.Migrations.Products");
                        sqlOptions.EnableRetryOnFailure();
                    }));
        }

        var usersEnvironmentVariable = Environment.GetEnvironmentVariable("CONNECTION_STRING_USERS");
        if (!string.IsNullOrWhiteSpace(usersEnvironmentVariable))
        {
            services.AddDbContext<UsersDbContext>(options =>
                options.UseSqlServer(
                    usersEnvironmentVariable,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("BPSEE.Experiment.Migrations.Users");
                        sqlOptions.EnableRetryOnFailure();
                    }));
        }

        return services;
    }
}