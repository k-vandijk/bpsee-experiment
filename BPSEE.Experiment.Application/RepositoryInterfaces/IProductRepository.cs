using BPSEE.Experiment.Domain.Entities;

namespace BPSEE.Experiment.Application.RepositoryInterfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync(CancellationToken ct = default);
    Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Product product, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}