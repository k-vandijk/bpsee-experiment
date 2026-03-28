using BPSEE.Experiment.Application.Dtos.Order;

namespace BPSEE.Experiment.Application.ServiceInterfaces;

public interface IOrderService
{
    Task<List<OrderDto>> GetAllAsync(CancellationToken ct = default);
    Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<OrderDto> CreateAsync(CreateOrderRequest request, CancellationToken ct = default);
    Task DeleteAllAsync(CancellationToken ct = default);
}