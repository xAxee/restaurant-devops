namespace Restaurant.Api.Persistence.Entities;

public sealed class Product
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }
}
