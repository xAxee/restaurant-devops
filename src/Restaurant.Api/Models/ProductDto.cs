namespace Restaurant.Api.Models;

public sealed record ProductDto(
    Guid Id,
    string Name,
    string? Description,
    decimal Price);
