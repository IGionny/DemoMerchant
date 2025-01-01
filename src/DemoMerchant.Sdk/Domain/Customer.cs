using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DemoMerchant.Sdk.Domain;

[Comment("Customer entity: contains the user information")]
public class Customer : AbsEntity
{
    [MaxLength(255)] [Required] public string FirstName { get; set; } = string.Empty;
    [MaxLength(255)] [Required] public string LastName { get; set; } = string.Empty;
    [MaxLength(255)][EmailAddress] public string Email { get; set; } = string.Empty;
    public List<Address> Addresses { get; set; } = new List<Address>();
}