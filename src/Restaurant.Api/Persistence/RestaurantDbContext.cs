using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Persistence.Entities;

namespace Restaurant.Api.Persistence;

public sealed class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
    : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var product = modelBuilder.Entity<Product>();
        product.ToTable("products");
        product.HasKey(item => item.Id);
        product.Property(item => item.Name).HasMaxLength(200).IsRequired();
        product.Property(item => item.Description).HasMaxLength(2000);
        product.Property(item => item.Price).HasPrecision(10, 2);
    }
}
