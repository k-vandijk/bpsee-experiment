using BPSEE.Experiment.Application.RepositoryInterfaces;
using BPSEE.Experiment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BPSEE.Experiment.Infrastructure.Repositories;

public class Repository<TEntity, TContext> : IRepository<TEntity>
    where TEntity : BaseEntity
    where TContext : DbContext
{
    protected readonly TContext Context;

    public Repository(TContext context)
    {
        Context = context;
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken ct = default)
        => await Context.Set<TEntity>().ToListAsync(ct);

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await Context.Set<TEntity>().FindAsync([id], ct);

    public async Task AddAsync(TEntity entity, CancellationToken ct = default)
        => await Context.Set<TEntity>().AddAsync(entity, ct);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await Context.SaveChangesAsync(ct);
}
