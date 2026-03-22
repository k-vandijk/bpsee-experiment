using BPSEE.Experiment.Application.RepositoryInterfaces;
using BPSEE.Experiment.Application.ServiceInterfaces;
using BPSEE.Experiment.Domain.Contexts;
using BPSEE.Experiment.Infrastructure.Repositories;
using BPSEE.Experiment.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BPSEE.Experiment.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CONNECTION_STRING_MONOLITHIC")))
        {
            services.AddScoped<IUserRepository, UserRepository<MonolithicDbContext>>();
            services.AddScoped<IProductRepository, ProductRepository<MonolithicDbContext>>();
            services.AddScoped<IOrderRepository, OrderRepository<MonolithicDbContext>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
        }

        if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CONNECTION_STRING_USERS")))
        {
            services.AddScoped<IUserRepository, UserRepository<UsersDbContext>>();
            services.AddScoped<IUserService, UserService>();
        }

        if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CONNECTION_STRING_PRODUCTS")))
        {
            services.AddScoped<IProductRepository, ProductRepository<ProductsDbContext>>();
            services.AddScoped<IProductService, ProductService>();
        }

        if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CONNECTION_STRING_ORDERS")))
        {
            services.AddScoped<IOrderRepository, OrderRepository<OrdersDbContext>>();
            services.AddScoped<IOrderService, OrderService>();
        }

        return services;
    }
}