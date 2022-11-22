using JobOpportunityMicroservice.Domain.Enums;

namespace JobOpportunityMicroservice.Domain;

public class JobAdvertisement : BaseEntity
{
    public string? Title { get; set; }

    public string? Company { get; set; }
    
    public string? Description { get; set; }
    
    public Modality? Modality { get; set; }
    
    public string? Benefit { get; set; }
    
    public decimal? MinPayRange { get; set; }
    
    public decimal? MaxPayRange { get; set; }
    
    public string? Requerements { get; set; }
    
    public int? MonthlyHours { get; set; }
    
    public string? Email { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public string? Link { get; set; }
    
    public DateTime? DateLimit { get; set; }
    
    public bool? IsActive { get; set; }

    public string? UserId { get; set; }

    // Relationship
    public virtual Address? Address { get; set; }
    public virtual List<JobCategory> JobCategories { get; set; } = new();
}
