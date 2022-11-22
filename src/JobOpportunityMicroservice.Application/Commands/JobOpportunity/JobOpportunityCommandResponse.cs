using System.Text.Json.Serialization;

namespace JobOpportunityMicroservice.Application.Commands.JobOpportunity;

public record JobOpportunityCommandResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("company")]
    public string Company { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("modality")]
    public string Modality { get; set; }
    
    [JsonPropertyName("benefit")]
    public string Benefit { get; set; }
    
    [JsonPropertyName("min_payrange")]
    public decimal MinPayRange { get; set; }
    
    [JsonPropertyName("max_payrange")]
    public decimal MaxPayRange { get; set; }
    
    [JsonPropertyName("requerements")]
    public string Requerements { get; set; }
    
    [JsonPropertyName("monthly_hours")]
    public int MonthlyHours { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; }
    
    [JsonPropertyName("link")]
    public string Link { get; set; }
    
    [JsonPropertyName("date_limit")]
    public string DateLimit { get; set; }
    
    [JsonPropertyName("is_active")]
    public bool IsActive { get; set; }
    
    [JsonPropertyName("user_id")]
    public string UserId { get; set; }
    
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; }
    
    [JsonPropertyName("updated_at")]
    public string UpdatedAt { get; set; }
    
    [JsonPropertyName("address")]
    public AddressResponse Address { get; set; }
    
    [JsonPropertyName("categories")]
    public List<string> Categories { get; set; }
}