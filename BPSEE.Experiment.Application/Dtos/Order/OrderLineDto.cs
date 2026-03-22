namespace BPSEE.Experiment.Application.Dtos.Order;

public record OrderLineDto(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity,
    decimal LineTotal);