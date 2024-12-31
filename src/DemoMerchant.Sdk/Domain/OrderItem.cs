namespace DemoMerchant.Sdk.Domain;

/// <summary>
/// Represents an item in an order.
/// Used to link a product to an order with a quantity and price.
/// </summary>
public class OrderItem
{
    /// <summary>
    /// Useful for identifying the item in UI
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Useful for positioning the item in the order.
    /// </summary>
    public int Index { get; set; }
    public Product? Product { get; set; }
    public decimal Quantity { get; set; } = 1;
    public decimal Price { get; set; } = 0;
}