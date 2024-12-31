using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DemoMerchant.Sdk.Domain;

[Comment("Orders")]
public class Order : AbsEntity
{
    [Required]
    public Customer? Customer { get; set; }
    
    [Required]
    public DateTime? OrderDate { get; set; }
    
    [Required]
    public Address? ShippingAddress { get; set; }
    
    
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}