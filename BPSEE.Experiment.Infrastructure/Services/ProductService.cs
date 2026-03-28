using BPSEE.Experiment.Application.Dtos.Product;
using BPSEE.Experiment.Application.RepositoryInterfaces;
using BPSEE.Experiment.Application.ServiceInterfaces;
using BPSEE.Experiment.Domain.Entities;

namespace BPSEE.Experiment.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProductDto>> GetAllAsync(CancellationToken ct = default)
    {
        var products = await _repository.GetAllAsync(ct);
        return products.Select(ToDto).ToList();
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var product = await _repository.GetByIdAsync(id, ct);
        return product is null ? null : ToDto(product);
    }

    public async Task<ProductDto> CreateAsync(CreateProductRequest request, CancellationToken ct = default)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(product, ct);
        await _repository.SaveChangesAsync(ct);

        return ToDto(product);
    }

    public Task DeleteAllAsync(CancellationToken ct = default)
        => _repository.DeleteAllAsync(ct);

    private static ProductDto ToDto(Product p) => new(p.Id, p.Name, p.Description, p.Price, p.Stock);
}
