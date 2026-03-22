using BPSEE.Experiment.Application.Dtos.User;

namespace BPSEE.Experiment.Application.ClientInterfaces;

public interface IUserServiceClient
{
    Task<UserDto?> GetByIdAsync(Guid userId, CancellationToken ct = default);
}