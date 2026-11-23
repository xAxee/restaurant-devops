using Restaurant.Api.Models;
using Restaurant.Api.Services;

namespace Restaurant.Api.Endpoints;

public static class ProductsEndpoints
{
    public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var products = endpoints.MapGroup("/products")
            .WithTags("Products");

        products.MapGet("/", GetProducts);
        products.MapGet("/{id:guid}", GetProduct);
        products.MapPost("/", CreateProduct);
        products.MapDelete("/{id:guid}", DeleteProduct);

        return endpoints;
    }

    private static async Task<IResult> GetProducts(
        ProductService service,
        CancellationToken cancellationToken)
    {
        var products = await service.GetProductsAsync(cancellationToken);
        return Results.Ok(products);
    }

    private static async Task<IResult> GetProduct(
        Guid id,
        ProductService service,
        CancellationToken cancellationToken)
    {
        var product = await service.GetProductAsync(id, cancellationToken);
        return product is null ? Results.NotFound() : Results.Ok(product);
    }

    private static async Task<IResult> CreateProduct(
        CreateProductDto request,
        ProductService service,
        CancellationToken cancellationToken)
    {
        var errors = Validate(request);
        if (errors.Count > 0)
        {
            return Results.ValidationProblem(errors);
        }

        var product = await service.CreateProductAsync(request, cancellationToken);
        return Results.Created($"/products/{product.Id}", product);
    }

    private static async Task<IResult> DeleteProduct(
        Guid id,
        ProductService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteProductAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static Dictionary<string, string[]> Validate(CreateProductDto request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            errors["name"] = ["Name is required."];
        }

        if (request.Price < 0)
        {
            errors["price"] = ["Price must be zero or greater."];
        }

        return errors;
    }
}
