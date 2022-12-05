using JobOpportunityMicroservice.Application.Commands.JobOpportunity;
using MediatR;
using Newtonsoft.Json;

namespace JobOpportunityMicroservice.Application.Commands.UpdateJobOpportunity;

public record AddressRequest
{
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("street")]
    public string Street { get; set; }
    
    [JsonProperty("district")]
    public string District { get; set; }

    [JsonProperty("city")]
    public string City { get; set; }

    [JsonProperty("state")]
    public string State { get; set; }
    
    [JsonProperty("country")]
    public string Country { get; set; }
}

public record UpdateJobOpportunityCommand : IRequest<JobOpportunityCommandResponse>
{
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("is_active")]
    public string IsActive { get; set; }
    
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
    public decimal? MinPayRange { get; set; }
    
    [JsonProperty("max_payrange")]
    public decimal? MaxPayRange { get; set; }
    
    [JsonProperty("requerements")]
    public string Requerements { get; set; }
    
    [JsonProperty("monthly_hours")]
    public int? MonthlyHours { get; set; }
    
    [JsonProperty("email")]
    public string Email { get; set; }
    
    [JsonProperty("phone_number")]
    public string PhoneNumber { get; set; }
    
    [JsonProperty("link")]
    public string Link { get; set; }
    
    [JsonProperty("date_limit")]
    public string DateLimit { get; set; }

    [JsonProperty("address")]
    public AddressRequest Address { get; set; }

    [JsonProperty("categories")]
    public List<string> Categories { get; set; }
}