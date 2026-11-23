namespace Restaurant.Api.Models;

public sealed record CreateProductDto(
    string? Name,
    string? Description,
    decimal Price);
