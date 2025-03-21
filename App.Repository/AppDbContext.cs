using App.Repository.Products;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App.Repository;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly()); 

        base.OnModelCreating(modelBuilder);
    }
}
