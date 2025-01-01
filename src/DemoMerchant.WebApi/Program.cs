using AddressServices;
using CustomerServices;
using DemoMerchant.Sdk;
using DemoMerchant.WebApi.Helpers;
using DemoMerchant.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using OrderServices;
using ProductServices;
using Serilog;

//First ensure folders:
FsHelper.EnsureDirectory("App_Data");
FsHelper.EnsureDirectory("App_Data", "Data");
FsHelper.EnsureDirectory("App_Data", "Logs");

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSerilog();

//Configure services (they are scoped because they are used for the lifetime of the request)
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();

//This is a transient service because it is used only once
builder.Services.AddTransient<IApplicationBootstrapService, ApplicationBootstrapService>();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var currentPath = Directory.GetCurrentDirectory();
    var appDataPath = Path.Combine(currentPath, "App_Data", "Data");
    var dbPath = Path.Combine(appDataPath, "DemoMerchant.db");
    options.UseSqlite($"Data Source={dbPath}");
});

//This is necessary with the TypedResult
builder.Services.ConfigureHttpJsonOptions((op) =>
{
    op.SerializerOptions.PropertyNamingPolicy = null; // This will preserve the original property names
});


var app = builder.Build();

// After the application starts, we will migrate the database automatically
app.Lifetime.ApplicationStarted.Register(async () =>
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var bootstrapService = services.GetRequiredService<IApplicationBootstrapService>();
        await bootstrapService.MigrateAndPrepareDatabaseAsync();
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //Map api via OpenApi (.NET 9)
    app.MapOpenApi();
    
    //Using Swagger to have the UI (go to /swagger/index.html)
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/v1.json", "Demo Merchant API"); });
}

app.UseHttpsRedirection();

app.MapControllers();

Log.Information("Starting web application");

try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}