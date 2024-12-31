using CustomerServices;
using DemoMerchant.Sdk;
using Microsoft.EntityFrameworkCore;
using OrderServices;
using ProductServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Configure services (they are scoped because they are used for the lifetime of the request)
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var currentPath = Directory.GetCurrentDirectory();
    var appDataPath = Path.Combine(currentPath, "App_Data");
    if (!Directory.Exists(appDataPath))
    {
        Directory.CreateDirectory(appDataPath);
    }
    var dbPath = Path.Combine(appDataPath, "DemoMerchant.db");
    options.UseSqlite($"Data Source={dbPath}");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();