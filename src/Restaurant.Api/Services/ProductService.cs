using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Models;
using Restaurant.Api.Persistence;
using Restaurant.Api.Persistence.Entities;

namespace Restaurant.Api.Services;

public sealed class ProductService(RestaurantDbContext database)
{
    public async Task<IReadOnlyList<ProductDto>> GetProductsAsync(CancellationToken cancellationToken) =>
        await database.Products
            .AsNoTracking()
            .OrderBy(product => product.Name)
            .Select(product => new ProductDto(
                product.Id,
                product.Name,
                product.Description,
                product.Price))
            .ToListAsync(cancellationToken);

    public async Task<ProductDto?> GetProductAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await database.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(product => product.Id == id, cancellationToken);

        return product is null ? null : ToDto(product);
    }

    public async Task<ProductDto> CreateProductAsync(
        CreateProductDto request,
        CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name!.Trim(),
            Description = string.IsNullOrWhiteSpace(request.Description)
                ? null
                : request.Description.Trim(),
            Price = request.Price
        };

        database.Products.Add(product);
        await database.SaveChangesAsync(cancellationToken);

        return ToDto(product);
    }

    public async Task<bool> DeleteProductAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await database.Products.FindAsync([id], cancellationToken);
        if (product is null)
        {
            return false;
        }

        database.Products.Remove(product);
        await database.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static ProductDto ToDto(Product product) =>
        new(product.Id, product.Name, product.Description, product.Price);
}
