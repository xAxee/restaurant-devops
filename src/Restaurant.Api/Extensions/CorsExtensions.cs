namespace Restaurant.Api.Extensions;

public static class CorsExtensions
{
    public const string AngularDevelopmentPolicy = "AngularDevelopment";

    public static IServiceCollection AddApiCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(AngularDevelopmentPolicy, policy =>
            {
                policy
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }
}
