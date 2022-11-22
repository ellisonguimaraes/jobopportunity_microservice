using System.Text.Json.Serialization;

namespace JobOpportunityMicroservice.Application.Models;

public class BaseResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }
    
    [JsonPropertyName("updated_at")]
    public DateTime? UpdateAt { get; set; }
}