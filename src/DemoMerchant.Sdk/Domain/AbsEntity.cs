namespace DemoMerchant.Sdk.Domain;

public abstract class AbsEntity
{
    public Guid? Id { get; set; }
    
    /// <summary>
    /// A minimal audit trail
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    
    /// <summary>
    /// Permit Soft-Delete
    /// </summary>
    public bool IsDeleted { get; set; }
}