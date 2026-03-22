namespace BPSEE.Experiment.Application.Dtos.User;

public record UserDto(
    Guid Id,
    string Name,
    string Email);