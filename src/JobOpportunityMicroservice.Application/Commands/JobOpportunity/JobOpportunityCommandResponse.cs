using Newtonsoft.Json;

namespace JobOpportunityMicroservice.Application.Commands.JobOpportunity;

public record JobOpportunityCommandResponse
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
    
    [JsonProperty("title")]
    public string Title { get; set; }
    
    [JsonProperty("company")]
    public string Company { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }
    
    [JsonProperty("modality")]
    public string Modality { get; set; }
    
    [JsonProperty("benefit")]
    public string Benefit { get; set; }
    
    [JsonProperty("min_payrange")]
    public decimal MinPayRange { get; set; }
    
    [JsonProperty("max_payrange")]
    public decimal MaxPayRange { get; set; }
    
    [JsonProperty("requerements")]
    public string Requerements { get; set; }
    
    [JsonProperty("monthly_hours")]
    public int MonthlyHours { get; set; }
    
    [JsonProperty("email")]
    public string Email { get; set; }
    
    [JsonProperty("phone_number")]
    public string PhoneNumber { get; set; }
    
    [JsonProperty("link")]
    public string Link { get; set; }
    
    [JsonProperty("date_limit")]
    public string DateLimit { get; set; }
    
    [JsonProperty("is_active")]
    public bool IsActive { get; set; }
    
    [JsonProperty("user_id")]
    public string UserId { get; set; }
    
    [JsonProperty("created_at")]
    public string CreatedAt { get; set; }
    
    [JsonProperty("updated_at")]
    public string UpdatedAt { get; set; }
    
    [JsonProperty("address")]
    public AddressResponse Address { get; set; }
    
    [JsonProperty("categories")]
    public List<string> Categories { get; set; }
}