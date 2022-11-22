using System.Text.Json.Serialization;
using JobOpportunityMicroservice.Application.Commands.JobOpportunity;
using MediatR;

namespace JobOpportunityMicroservice.Application.Commands.AddJobOpportunity;

public record struct AddressRequest
{
    [JsonPropertyName("street")]
    public string Street { get; set; }
    
    [JsonPropertyName("district")]
    public string District { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }
    
    [JsonPropertyName("country")]
    public string Country { get; set; }
}

public record struct AddJobOpportunityCommand : IRequest<JobOpportunityCommandResponse>
{
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
    public decimal? MinPayRange { get; set; }
    
    [JsonPropertyName("max_payrange")]
    public decimal? MaxPayRange { get; set; }
    
    [JsonPropertyName("requerements")]
    public string Requerements { get; set; }
    
    [JsonPropertyName("monthly_hours")]
    public int? MonthlyHours { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; }
    
    [JsonPropertyName("link")]
    public string Link { get; set; }
    
    [JsonPropertyName("date_limit")]
    public string DateLimit { get; set; }

    [JsonPropertyName("address")]
    public AddressRequest Address { get; set; }

    [JsonPropertyName("categories")]
    public List<string> Categories { get; set; }
}