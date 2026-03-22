using BPSEE.Experiment.Application.Dtos.User;
using BPSEE.Experiment.Application.RepositoryInterfaces;
using BPSEE.Experiment.Application.ServiceInterfaces;
using BPSEE.Experiment.Domain.Entities;

namespace BPSEE.Experiment.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UserDto>> GetAllAsync(CancellationToken ct = default)
    {
        var users = await _repository.GetAllAsync(ct);
        return users.Select(ToDto).ToList();
    }

    public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var user = await _repository.GetByIdAsync(id, ct);
        return user is null ? null : ToDto(user);
    }

    public async Task<UserDto> CreateAsync(CreateUserRequest request, CancellationToken ct = default)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(user, ct);
        await _repository.SaveChangesAsync(ct);

        return ToDto(user);
    }

    private static UserDto ToDto(User user) => new(user.Id, user.Name, user.Email);
}
