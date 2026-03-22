namespace BPSEE.Experiment.Application.RepositoryInterfaces;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync(CancellationToken ct = default);
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(T entity, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
