using DemoMerchant.Sdk.Domain;

namespace DemoMerchant.ServicesTests;

public static class DataMother
{
    public static ProductCategory CreateProductCategory()
    {
        return new ProductCategory()
        {
            Id = new Guid("02196E09-3DD7-409C-AC9E-D77C1B77E4A0"),
            Name = "Category 1",
            CreatedAt = new DateTime(2024, 12, 26, 12, 26, 0, DateTimeKind.Utc),
        };
    }

    public static Product CreateProduct()
    {
        var category = CreateProductCategory();
        return new Product()
        {
            Id = new Guid("02196E09-3DD7-409C-AC9E-D77C1B77E4A0"),
            Name = "Product 1",
            Description = "A really nice product",
            Category = category,
            CreatedAt = new DateTime(2024, 12, 26, 12, 26, 0, DateTimeKind.Utc),
        };
    }

    public static Address CreateAddress()
    {
        return new Address
        {
            Id = new Guid("20F72CCE-BDDD-4090-9C01-C8430679E540"),
            AddressLine1 = "Via Roma",
            AddressLine2 = null,
            State = null,
            City = "Roma",
            ZipCode = "00100",
            Country = "Italy",
            CreatedAt = new DateTime(2024, 12, 26, 12, 26, 0, DateTimeKind.Utc),
        };
    }

    public static Customer CreateCustomer()
    {
        var address = CreateAddress();
        var customer = new Customer
        {
            Id = new Guid("3CE0C315-6E93-4E86-A9C0-7930BF7621DA"),
            FirstName = "Mario",
            LastName = "Rossi",
            Email = "r.mario@gmail.com",
            CreatedAt = new DateTime(2024, 12, 26, 12, 26, 0, DateTimeKind.Utc),
        };

        customer.Addresses.Add(address);
        return customer;
    }

    public static Order CreateOrder()
    {
        var customer = CreateCustomer();
        var address = CreateAddress();
        var product = CreateProduct();
        return new Order
        {
            Id = new Guid("8106E426-9E02-434B-80C8-C068233468A4"),
            Customer = customer,
            OrderDate = new DateTime(2024, 12, 26, 12, 29, 0, DateTimeKind.Utc),
            ShippingAddress = address,
            Items = new List<OrderItem>
            {
                new OrderItem
                {
                    Product = product,
                    Quantity = 1,
                    Price = 100,
                }
            },
            CreatedAt = new DateTime(2024, 12, 26, 12, 26, 0, DateTimeKind.Utc),
        };
    }
}