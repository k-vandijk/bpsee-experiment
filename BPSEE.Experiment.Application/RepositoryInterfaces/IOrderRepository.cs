using BPSEE.Experiment.Domain.Entities;

namespace BPSEE.Experiment.Application.RepositoryInterfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<List<Order>> GetAllWithLinesAsync(CancellationToken ct = default);
    Task<Order?> GetByIdWithLinesAsync(Guid id, CancellationToken ct = default);
}