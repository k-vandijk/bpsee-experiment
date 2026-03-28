using BPSEE.Experiment.Application.Dtos.Product;

namespace BPSEE.Experiment.Application.ServiceInterfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync(CancellationToken ct = default);
    Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ProductDto> CreateAsync(CreateProductRequest request, CancellationToken ct = default);
    Task DeleteAllAsync(CancellationToken ct = default);
}