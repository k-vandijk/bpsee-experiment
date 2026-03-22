using BPSEE.Experiment.Domain.Entities;

namespace BPSEE.Experiment.Application.RepositoryInterfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync(CancellationToken ct = default);
    Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Order order, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}