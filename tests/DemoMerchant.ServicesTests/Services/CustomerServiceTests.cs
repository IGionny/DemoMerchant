using CustomerServices;
using DemoMerchant.Sdk;
using DemoMerchant.Sdk.Domain;
using FluentAssertions;


namespace DemoMerchant.ServicesTests.Services;

public class CustomerServiceTests : AbsCrudServiceTests<Customer, CustomerService>
{
    protected override CustomerService CreateService(AppDbContext context)
    {
        return new CustomerService(context);
    }

    protected override void CreateAndGetAssertions(Customer entity, Customer foundEntity)
    {
        foundEntity.Addresses.Should().HaveCount(1);
    }

    protected override Customer CreateEntity()
    {
        var customer = DataMother.CreateCustomer();
        customer.Id = Guid.NewGuid();
        return customer;
    }
}