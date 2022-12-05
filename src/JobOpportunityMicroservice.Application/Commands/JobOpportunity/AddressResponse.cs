using Newtonsoft.Json;

namespace JobOpportunityMicroservice.Application.Commands.JobOpportunity;

public record AddressResponse
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
    
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
    
    [JsonProperty("created_at")]
    public string CreatedAt { get; set; }
    
    [JsonProperty("updated_at")]
    public string UpdatedAt { get; set; }
}