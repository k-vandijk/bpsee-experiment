using BPSEE.Experiment.Application.Dtos.Order;
using BPSEE.Experiment.Application.RepositoryInterfaces;
using BPSEE.Experiment.Application.ServiceInterfaces;
using BPSEE.Experiment.Domain.Entities;

namespace BPSEE.Experiment.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<OrderDto>> GetAllAsync(CancellationToken ct = default)
    {
        var orders = await _repository.GetAllWithLinesAsync(ct);
        return orders.Select(ToDto).ToList();
    }

    public async Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var order = await _repository.GetByIdWithLinesAsync(id, ct);
        return order is null ? null : ToDto(order);
    }

    public async Task<OrderDto> CreateAsync(CreateOrderRequest request, CancellationToken ct = default)
    {
        var lines = request.Lines.Select(l => new OrderLine
        {
            Id = Guid.NewGuid(),
            ProductId = l.ProductId,
            Quantity = l.Quantity,
            UnitPrice = l.UnitPrice,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }).ToList();

        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            TotalAmount = lines.Sum(l => l.UnitPrice * l.Quantity),
            Status = "Pending",
            OrderLines = lines,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(order, ct);
        await _repository.SaveChangesAsync(ct);

        return ToDto(order);
    }

    public Task DeleteAllAsync(CancellationToken ct = default)
        => _repository.DeleteAllAsync(ct);

    private static OrderDto ToDto(Order o) => new(
        o.Id,
        o.UserId,
        o.TotalAmount,
        o.Status,
        o.CreatedAt,
        o.OrderLines.Select(l => new OrderLineDto(
            l.ProductId,
            l.Product?.Name ?? string.Empty,
            l.UnitPrice,
            l.Quantity,
            l.LineTotal)).ToList());
}
