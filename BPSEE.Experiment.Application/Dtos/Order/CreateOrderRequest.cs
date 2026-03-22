namespace BPSEE.Experiment.Application.Dtos.Order;

public record CreateOrderRequest(
    Guid UserId,
    List<CreateOrderLineRequest> Lines);