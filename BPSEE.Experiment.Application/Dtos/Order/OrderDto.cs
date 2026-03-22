namespace BPSEE.Experiment.Application.Dtos.Order;

public record OrderDto(
    Guid Id,
    Guid UserId,
    decimal TotalAmount,
    string Status,
    DateTime CreatedAtUtc,
    List<OrderLineDto> Lines);