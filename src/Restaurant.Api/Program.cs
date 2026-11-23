using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Endpoints;
using Restaurant.Api.Extensions;
using Restaurant.Api.Persistence;
using Restaurant.Api.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RestaurantDatabase")
    ?? throw new InvalidOperationException("Connection string 'RestaurantDatabase' is missing.");

builder.Services.AddOpenApi();
builder.Services.AddApiCors();
builder.Services.AddDbContext<RestaurantDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(CorsExtensions.AngularDevelopmentPolicy);

app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }))
    .WithTags("Health");

app.MapProductsEndpoints();

await using (var scope = app.Services.CreateAsyncScope())
{
    var database = scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();
    await database.Database.EnsureCreatedAsync();
}

app.Run();
