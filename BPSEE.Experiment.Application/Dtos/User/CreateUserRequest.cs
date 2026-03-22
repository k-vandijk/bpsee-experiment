namespace BPSEE.Experiment.Application.Dtos.User;

public record CreateUserRequest(
    string Name,
    string Email);