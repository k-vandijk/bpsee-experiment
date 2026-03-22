using BPSEE.Experiment.Domain.Entities;

namespace BPSEE.Experiment.Application.RepositoryInterfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync(CancellationToken ct = default);
    Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(User user, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}