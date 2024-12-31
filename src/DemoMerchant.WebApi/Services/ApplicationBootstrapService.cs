using DemoMerchant.Sdk;
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
    }
}