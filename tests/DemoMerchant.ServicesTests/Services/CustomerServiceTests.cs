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

    protected override void UpdateEntity(Customer entity)
    {
        entity.Email = "foo@foo.com";
    }

    protected override void AssertionsAfterUpdate(Customer entity, Customer foundEntity)
    {
        foundEntity.Email.Should().Be("foo@foo.com");
    }

    protected override void CreateAndGetAssertions(Customer entity, Customer foundEntity)
    {
        foundEntity.Addresses.Should().HaveCount(1);
        foundEntity.Addresses[0].Should().NotBeNull();
        foundEntity.Addresses[0].Id.Should().NotBeEmpty();
        foundEntity.Addresses[0].CustomerId.Should().Be(foundEntity.Id);
    }

    protected override Customer CreateEntity()
    {
        var customer = DataMother.CreateCustomer();
        customer.Id = null;
        customer.Addresses[0].Id = null;
        return customer;
    }
}