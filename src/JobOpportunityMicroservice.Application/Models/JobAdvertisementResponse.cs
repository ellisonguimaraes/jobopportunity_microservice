using System.Text.Json.Serialization;
using JobOpportunityMicroservice.Domain.Enums;

namespace JobOpportunityMicroservice.Application.Models;

public class JobAdvertisementResponse : BaseResponse
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("company_name")]
    public string Company { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("modality")]
    public Modality Modality { get; set; }
    
    [JsonPropertyName("benefit")]
    public string Benefit { get; set; }
    
    [JsonPropertyName("min_pay_range")]
    public decimal MinPayRange { get; set; }
    
    [JsonPropertyName("max_pay_range")]
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
    public DateTime DateLimit { get; set; }
    
    [JsonPropertyName("is_active")]
    public bool IsActive { get; set; }
    
    [JsonPropertyName("user_id")]
    public string UserId { get; set; }
    
    [JsonPropertyName("address")]
    public AddressResponse Address { get; set; }
    
    [JsonPropertyName("categories")]
    public List<CategoryResponse> Categories { get; set; }
}