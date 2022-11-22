namespace JobOpportunityMicroservice.Domain;

public class JobCategory : BaseEntity
{
    // Relationship
    public Guid JobAdvertisementId { get; set; }
    public virtual JobAdvertisement JobAdvertisement { get; set; }
    
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }
}
