using DemoMerchant.Sdk.Domain;
using Microsoft.EntityFrameworkCore;

namespace DemoMerchant.Sdk;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        //https://learn.microsoft.com/en-us/ef/core/saving/cascade-delete#database-cascade-limitations
        modelBuilder.Entity<Address>()
            .HasOne<Customer>()
            .WithMany(c => c.Addresses)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}