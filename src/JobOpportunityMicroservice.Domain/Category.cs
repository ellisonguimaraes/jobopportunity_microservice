namespace JobOpportunityMicroservice.Domain;

public class Category : BaseEntity
{
    public string Name { get; set; }

    // Relationship
    public virtual List<JobCategory> JobCategories { get; set; }
}
