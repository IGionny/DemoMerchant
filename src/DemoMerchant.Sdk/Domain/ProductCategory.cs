using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DemoMerchant.Sdk.Domain;

[Comment("Product category dictionary")]
public class ProductCategory : AbsEntity
{
    [Required] [MaxLength(255)] public string Name { get; set; } = string.Empty;
}