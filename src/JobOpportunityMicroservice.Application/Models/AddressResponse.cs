using System.Text.Json.Serialization;

namespace JobOpportunityMicroservice.Application.Models;

public class AddressResponse : BaseResponse
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