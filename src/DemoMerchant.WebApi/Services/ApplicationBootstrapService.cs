using DemoMerchant.Sdk;
using DemoMerchant.Sdk.Domain;
using Microsoft.EntityFrameworkCore;

namespace DemoMerchant.WebApi.Services;

public interface IApplicationBootstrapService
{
    Task MigrateAndPrepareDatabaseAsync();
}

public class ApplicationBootstrapService : IApplicationBootstrapService
{
    private readonly ILogger<ApplicationBootstrapService> _logger;
    private readonly AppDbContext _appDbContext;

    public ApplicationBootstrapService(ILogger<ApplicationBootstrapService> logger, AppDbContext appDbContext)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
    }

    public async Task MigrateAndPrepareDatabaseAsync()
    {
        _logger.LogInformation("Start verifying schema database and initial data...");
        // Apply any pending migration and creates db IF it doesn't exist
        try
        {
            await _appDbContext.Database.MigrateAsync();
            _logger.LogInformation("Database schema verified");
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Unhandled exception during database schema migration");
            throw;
        }

        // Seed initial data
        try
        {
            await SeedInitialDataAsync();
            _logger.LogInformation("Initial data seeded");
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Unhandled exception during seeding initial data");
            throw;
        }
    }

    protected internal async Task SeedInitialDataAsync()
    {
        //Product categories:
        var electronics = new ProductCategory { Name = "Electronics" };
        var clothing = new ProductCategory { Name = "Clothing" };
        var books = new ProductCategory { Name = "Books" };
        var furniture = new ProductCategory { Name = "Furniture" };

        var productCategories = new List<ProductCategory>
        {
            electronics,
            clothing,
            books,
            furniture
        };

        //Products:
        var products = new List<Product>
        {
            new Product { Name = "Laptop", Category = electronics },
            new Product { Name = "T-shirt", Category = clothing },
            new Product { Name = "Book", Category = books },
            new Product { Name = "Chair", Category = furniture }
        };

        //Customers and addresses
        var customer1 = new Customer
        {
            FirstName = "John", LastName = "Doe", Email = "j.doe@gmail.com", Addresses =
            [
                new Address()
                {
                    AddressLine1 = "123 Main St",
                    City = "New York",
                    State = "NY",
                    ZipCode = "10001",
                    Country = "USA"
                }
            ]
        };

        var customers = new List<Customer>
        {
            customer1
        };

        //Inserts
        if (!(await _appDbContext.ProductCategories.AnyAsync()))
        {
            await _appDbContext.ProductCategories.AddRangeAsync(productCategories);
        }

        if (!(await _appDbContext.Products.AnyAsync()))
        {
            await _appDbContext.Products.AddRangeAsync(products);
        }

        if (!(await _appDbContext.Customers.AnyAsync()))
        {
            await _appDbContext.Customers.AddRangeAsync(customers);
        }

        //Save changes:
        await _appDbContext.SaveChangesAsync();
    }
}