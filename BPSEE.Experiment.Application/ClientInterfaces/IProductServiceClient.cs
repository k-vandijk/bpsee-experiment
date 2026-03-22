using BPSEE.Experiment.Application.Dtos.Product;

namespace BPSEE.Experiment.Application.ClientInterfaces;

public interface IProductServiceClient
{
    Task<ProductDto?> GetByIdAsync(Guid productId, CancellationToken ct = default);
}