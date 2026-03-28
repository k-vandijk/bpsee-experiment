using BPSEE.Experiment.Application.RepositoryInterfaces;
using BPSEE.Experiment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BPSEE.Experiment.Infrastructure.Repositories;

public class OrderRepository<TContext> : Repository<Order, TContext>, IOrderRepository
    where TContext : DbContext
{
    public OrderRepository(TContext context) : base(context)
    {
    }

    public async Task<List<Order>> GetAllWithLinesAsync(CancellationToken ct = default)
        => await Context.Set<Order>()
            .Include(o => o.OrderLines)
            .ToListAsync(ct);

    public async Task<Order?> GetByIdWithLinesAsync(Guid id, CancellationToken ct = default)
        => await Context.Set<Order>()
            .Include(o => o.OrderLines)
            .FirstOrDefaultAsync(o => o.Id == id, ct);

    public override async Task DeleteAllAsync(CancellationToken ct = default)
    {
        await Context.Set<OrderLine>().ExecuteDeleteAsync(ct);
        await Context.Set<Order>().ExecuteDeleteAsync(ct);
    }
}
