using DemoMerchant.Sdk;
using DemoMerchant.Sdk.Domain;
using FluentAssertions;
using OrderServices;

namespace DemoMerchant.ServicesTests.Services;

public class OrderServiceTests : AbsCrudServiceTests<Order, OrderService>
{
    protected override OrderService CreateService(AppDbContext context)
    {
        return new OrderService(context);
    }

    protected override void CreateAndGetAssertions(Order entity, Order foundEntity)
    {
        foundEntity.Customer.Should().NotBeNull();
        foundEntity.Customer!.Id.Should().Be(entity.Customer!.Id);
        foundEntity.Items.Should().HaveCount(1);
        foundEntity.Items[0]!.Product!.Id.Should().Be(entity.Items[0]!.Product!.Id);
        foundEntity.Items[0].Quantity.Should().Be(entity.Items[0].Quantity);
        foundEntity.Items[0].Price.Should().Be(entity.Items[0].Price);
    }


    protected override Order CreateEntity()
    {
        var newAddress = DataMother.CreateAddress();
        newAddress.Id = Guid.NewGuid();
        
        var order = DataMother.CreateOrder();
        order.Id = Guid.NewGuid();
        
        order.Customer!.Addresses.Add(newAddress);
        order.ShippingAddress = newAddress;
        
        return order;
    }
}