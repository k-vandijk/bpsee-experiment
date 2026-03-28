using BPSEE.Experiment.Application.Dtos.User;

namespace BPSEE.Experiment.Application.ServiceInterfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAllAsync(CancellationToken ct = default);
    Task<UserDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<UserDto> CreateAsync(CreateUserRequest request, CancellationToken ct = default);
    Task DeleteAllAsync(CancellationToken ct = default);
}