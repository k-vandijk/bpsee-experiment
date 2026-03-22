namespace BPSEE.Experiment.Application.Dtos.Order;

public record CreateOrderLineRequest(
    Guid ProductId,
    int Quantity);