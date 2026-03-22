namespace BPSEE.Experiment.Application.Dtos.Product;

public record ProductDto(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int Stock);