var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularDevelopment", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AngularDevelopment");

app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }))
    .WithName("Health")
    .WithTags("Health");

app.Run();
