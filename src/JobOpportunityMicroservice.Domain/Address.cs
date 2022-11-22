namespace JobOpportunityMicroservice.Domain;

public class Address : BaseEntity
{
    public string Street { get; set; }

    public string District { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    // Relationship
    public Guid JobAdvertisementId { get; set; }
    public virtual JobAdvertisement JobAdvertisement { get; set; }
}
