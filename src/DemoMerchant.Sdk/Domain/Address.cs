using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DemoMerchant.Sdk.Domain;

[Comment("Address entity: used by Customer and Order")]
public class Address : AbsEntity
{
    [MaxLength(255)] [Required] public string AddressLine1 { get; set; } = string.Empty;

    [MaxLength(255)] public string? AddressLine2 { get; set; }

    [MaxLength(255)] [Required] public string City { get; set; } = string.Empty;

    [MaxLength(255)] public string? State { get; set; }

    [MaxLength(50)] [Required] public string ZipCode { get; set; } = string.Empty;

    [MaxLength(255)] [Required] public string Country { get; set; } = string.Empty;
    
    
    /// <summary>
    /// A reference to the Customer entity
    /// </summary>
    public Guid? CustomerId { get; set; }
}