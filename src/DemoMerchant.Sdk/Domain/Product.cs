using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DemoMerchant.Sdk.Domain;

[Comment("Product catalogue")]
public class Product : AbsEntity
{
    [Required] [MaxLength(255)] public string Name { get; set; } = string.Empty;

    [MaxLength(500)] public string Description { get; set; } = string.Empty;
    public ProductCategory? Category { get; set; }
}