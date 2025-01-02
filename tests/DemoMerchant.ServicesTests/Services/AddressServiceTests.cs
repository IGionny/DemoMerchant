using AddressServices;
using DemoMerchant.Sdk;
using DemoMerchant.Sdk.Domain;
using FluentAssertions;

namespace DemoMerchant.ServicesTests.Services;

public class AddressServiceTests : AbsCrudServiceTests<Address, AddressService>
{
    protected override void UpdateEntity(Address entity)
    {
        entity.City = "Padova";
    }

    protected override void AssertionsAfterUpdate(Address entity, Address foundEntity)
    {
        foundEntity.City.Should().Be("Padova");
    }

    protected override AddressService CreateService(AppDbContext context)
    {
        return new AddressService(context);
    }

    protected override Address CreateEntity()
    {
        var address = DataMother.CreateAddress();
        address.Id = null;
        return address;
    }
}