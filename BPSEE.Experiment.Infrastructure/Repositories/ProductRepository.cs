using BPSEE.Experiment.Application.RepositoryInterfaces;
using BPSEE.Experiment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BPSEE.Experiment.Infrastructure.Repositories;

public class ProductRepository<TContext> : Repository<Product, TContext>, IProductRepository
    where TContext : DbContext
{
    public ProductRepository(TContext context) : base(context)
    {
    }
}
